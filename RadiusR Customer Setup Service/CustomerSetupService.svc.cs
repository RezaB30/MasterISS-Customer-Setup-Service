﻿using RadiusR_Customer_Setup_Service.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RadiusR_Customer_Setup_Service.Requests;
using RadiusR_Customer_Setup_Service.Responses;
using RadiusR.DB;
using RadiusR_Customer_Setup_Service.Enums;
using RadiusR_Customer_Setup_Service.Responses.Parameters;
using System.Data.Entity;
using RezaB.API.WebService.NLogExtentions;
using RezaB.TurkTelekom.WebServices.TTOYS;
using System.IO;
using RadiusR.DB.Enums.CustomerSetup;
using RadiusR.PDFForms;
using RadiusR.DB.DomainsCache;
using RadiusR.DB.ModelExtentions;
using RadiusR.FileManagement;
using RezaB.API.WebService;

namespace RadiusR_Customer_Setup_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class CustomerSetupService : ICustomerSetupService
    {
        private static readonly WebServiceLogger _logger = new WebServiceLogger("main");
        private static readonly WebServiceLogger _messageLogger = new WebServiceLogger("MessageTrace");

        public string GetKeyFragment(string username)
        {
            return KeyManager.GenerateKeyFragment(username, Properties.Settings.Default.CacheDuration);
        }

        public GetTaskListResponse GetTaskList(GetTaskListRequest request)
        {
            try
            {
                _messageLogger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<GetTaskListResponse>(request);
                }
                if (request.DateSpan.StartDateParsed == null || request.DateSpan.EndDateParsed == null)
                {
                    return CommonResponse.InvalidStartOrEndDateResponse<GetTaskListResponse>(user.PasswordHash, request);
                }
                if (request.DateSpan.EndDateParsed - request.DateSpan.StartDateParsed > TimeSpan.FromDays(30))
                {
                    return CommonResponse.InvalidTimespanResponse<GetTaskListResponse>(user.PasswordHash, request);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var tasks = db.CustomerSetupTasks.GetUserTasks(user.UserId).Where(task => task.TaskIssueDate >= request.DateSpan.StartDateParsed && DbFunctions.TruncateTime(task.TaskIssueDate) <= request.DateSpan.EndDateParsed)
                        .Include(task => task.Subscription.SubscriptionTelekomInfo).Include(task => task.Subscription.Address).Include(task => task.CustomerSetupStatusUpdates).Include(task => task.Subscription.Customer)
                        .Select(task => new
                        {
                            TaskNo = task.ID,
                            Subscription = task.Subscription,
                            XDSLType = task.XDSLType,
                            HasModem = task.HasModem,
                            ModemName = task.ModemName,
                            ReservationDate = task.CustomerSetupStatusUpdates.OrderByDescending(tu => tu.Date).Select(tu => tu.ReservationDate).FirstOrDefault(),
                            TaskIssueDate = task.TaskIssueDate,
                            LastConnection = task.Subscription.RadiusAccountings.OrderByDescending(ra => ra.StartTime).FirstOrDefault(),
                            TaskStatus = task.TaskStatus,
                            TaskType = task.TaskType,
                            Details = task.Details
                        }).ToList();

                    var taskList = tasks.Select(task => new SetupTask()
                    {
                        TaskNo = task.TaskNo,
                        SubscriberNo = task.Subscription.SubscriberNo,
                        BBK = task.Subscription.Address.ApartmentID.ToString(),
                        Province = task.Subscription.Address.ProvinceName,
                        City = task.Subscription.Address.DistrictName,
                        Address = task.Subscription.Address.AddressText,
                        XDSLNo = task.Subscription.SubscriptionTelekomInfo != null ? task.Subscription.SubscriptionTelekomInfo.SubscriptionNo : null,
                        PSTN = task.Subscription.SubscriptionTelekomInfo != null ? task.Subscription.SubscriptionTelekomInfo.PSTN : null,
                        XDSLType = task.XDSLType,
                        HasModem = task.HasModem,
                        ModemName = task.ModemName,
                        CustomerPhoneNo = task.Subscription.Customer.ContactPhoneNo,
                        ContactName = task.Subscription.ValidDisplayName,
                        ReservationDate = DataBinder.GetDateTimeString(task.ReservationDate),
                        TaskIssueDate = DataBinder.GetDateTimeString(task.TaskIssueDate),
                        LastConnectionDate = task.LastConnection != null ? DataBinder.GetDateTimeString(task.LastConnection.StartTime) : null,
                        CustomerType = task.Subscription.Customer.CustomerType,
                        TaskType = task.TaskType,
                        TaskStatus = task.TaskStatus,
                        Details = task.Details
                    });

                    _logger.LogInfo(request.Username, string.Empty);
                    return new GetTaskListResponse(user.PasswordHash, request)
                    {
                        ResponseMessage = new ServiceResponse()
                        {
                            ErrorCode = (int)ErrorCodes.Success,
                            ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("Success", CommonResponse.CreateCulture(request.Culture))
                        },
                        SetupTasks = taskList.ToList()
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<GetTaskListResponse>(request);
            }
        }

        public GetCustomerCredentialResponse GetCustomerCredentials(TaskNoRequest request)
        {
            try
            {
                _messageLogger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<GetCustomerCredentialResponse>(request);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.TaskNotFoundErrorResponse<GetCustomerCredentialResponse>(user.PasswordHash, request);
                    }
                    if (!task.CanBeUpdated())
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<GetCustomerCredentialResponse>(user.PasswordHash, request);
                    }

                    _logger.LogInfo(request.Username, task.Subscription.SubscriberNo);
                    return new GetCustomerCredentialResponse(user.PasswordHash, request)
                    {
                        ResponseMessage = new ServiceResponse()
                        {
                            ErrorCode = (int)ErrorCodes.Success,
                            ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("Success", CommonResponse.CreateCulture(request.Culture))
                        },
                        CustomerCredentials = new CustomerCredentials()
                        {
                            Username = task.Subscription.Username,
                            Password = task.Subscription.RadiusPassword
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<GetCustomerCredentialResponse>(request);
            }
        }

        public GetCustomerLineDetailsResponse GetCustomerLineDetails(TaskNoRequest request)
        {
            try
            {
                _messageLogger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<GetCustomerLineDetailsResponse>(request);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.TaskNotFoundErrorResponse<GetCustomerLineDetailsResponse>(user.PasswordHash, request);
                    }
                    if (!task.CanBeUpdated())
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<GetCustomerLineDetailsResponse>(user.PasswordHash, request);
                    }
                    var cachedDomain = DomainsCache.GetDomainByID(task.Subscription.DomainID);
                    if (task.Subscription.SubscriptionTelekomInfo == null || cachedDomain.TelekomCredential == null)
                    {
                        return CommonResponse.MissingCustomerDataResponse<GetCustomerLineDetailsResponse>(user.PasswordHash, request);
                    }

                    TTOYSServiceClient externalClient = new TTOYSServiceClient(cachedDomain.TelekomCredential.XDSLWebServiceUsernameInt, cachedDomain.TelekomCredential.XDSLWebServicePassword);
                    var response = externalClient.Check(task.Subscription.SubscriptionTelekomInfo.SubscriptionNo);
                    if (response.InternalException != null)
                    {
                        return CommonResponse.ExternalWebServiceErrorResponse<GetCustomerLineDetailsResponse>(user.PasswordHash, request);
                    }

                    _logger.LogInfo(request.Username, task.Subscription.SubscriberNo);
                    return new GetCustomerLineDetailsResponse(user.PasswordHash, request)
                    {
                        ResponseMessage = new ServiceResponse()
                        {
                            ErrorCode = (int)ErrorCodes.Success,
                            ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("Success", CommonResponse.CreateCulture(request.Culture))
                        },
                        CustomerLineDetails = new CustomerLineDetails()
                        {
                            XDSLNo = task.Subscription.SubscriptionTelekomInfo.SubscriptionNo,
                            IsActive = response.Data.OperationStatus == TTOYSServiceClient.OperationStatus.Open,
                            DownloadNoiseMargin = response.Data.NoiseRateDown,
                            UploadNoiseMargin = response.Data.NoiseRateUp,
                            CurrentDownloadSpeed = response.Data.CurrentDown,
                            CurrentUploadSpeed = response.Data.CurrentUp,
                            DownloadSpeedCapacity = response.Data.MaxDown,
                            UploadSpeedCapacity = response.Data.MaxUp
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<GetCustomerLineDetailsResponse>(request);
            }
        }

        public GetCustomerSessionInfoResponse GetCustomerSessionInfo(TaskNoRequest request)
        {
            try
            {
                _messageLogger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<GetCustomerSessionInfoResponse>(request);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.TaskNotFoundErrorResponse<GetCustomerSessionInfoResponse>(user.PasswordHash, request);
                    }
                    if (!task.CanBeUpdated())
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<GetCustomerSessionInfoResponse>(user.PasswordHash, request);
                    }

                    var firstSession = task.Subscription.RadiusAccountings.OrderBy(ra => ra.StartTime).FirstOrDefault();
                    _logger.LogInfo(request.Username, task.Subscription.SubscriberNo);
                    if (firstSession == null)
                    {
                        return new GetCustomerSessionInfoResponse(user.PasswordHash, request)
                        {
                            ResponseMessage = new ServiceResponse()
                            {
                                ErrorCode = (int)ErrorCodes.Success,
                                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("Success", CommonResponse.CreateCulture(request.Culture))
                            },
                            CustomerSessionInfo = new CustomerSessionInfo()
                            {
                                IsOnline = false
                            }
                        };
                    }

                    return new GetCustomerSessionInfoResponse(user.PasswordHash, request)
                    {
                        ResponseMessage = new ServiceResponse()
                        {
                            ErrorCode = (int)ErrorCodes.Success,
                            ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("Success", CommonResponse.CreateCulture(request.Culture))
                        },
                        CustomerSessionInfo = new CustomerSessionInfo()
                        {
                            IsOnline = !firstSession.StopTime.HasValue,
                            NASIPAddress = firstSession.NASIP,
                            SessionId = firstSession.SessionID,
                            SessionTime = TimeSpan.FromSeconds(firstSession.SessionTime).ToString(@"dd\.hh\:mm\:ss"),
                            SessionStart = DataBinder.GetDateTimeString(firstSession.StartTime),
                            IPAddress = firstSession.RadiusAccountingIPInfo != null ? firstSession.RadiusAccountingIPInfo.RealIP : null
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<GetCustomerSessionInfoResponse>(request);
            }
        }

        public ParameterlessResponse AddTaskStatusUpdate(AddTaskStatusUpdateRequest request)
        {
            try
            {
                _messageLogger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<ParameterlessResponse>(request);
                }
                if (!string.IsNullOrEmpty(request.TaskUpdate.ReservationDate) && !request.TaskUpdate.ReservationDateParsed.HasValue)
                {
                    return CommonResponse.InvalidReservationDateResponse<ParameterlessResponse>(user.PasswordHash, request);
                }
                if (!Enum.IsDefined(typeof(FaultCodes), (int)request.TaskUpdate.FaultCode))
                {
                    return CommonResponse.InvalidFaultCodeResponse<ParameterlessResponse>(user.PasswordHash, request);
                }
                if (request.TaskUpdate.Description != null && request.TaskUpdate.Description.Length > 450)
                {
                    return CommonResponse.DescriptionIsTooLongResponse<ParameterlessResponse>(user.PasswordHash, request);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.TaskUpdate.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.InvalidTaskNoResponse<ParameterlessResponse>(user.PasswordHash, request);
                    }
                    if (!task.CanChangeState((FaultCodes)request.TaskUpdate.FaultCode))
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<ParameterlessResponse>(user.PasswordHash, request);
                    }

                    var statusUpdate = new CustomerSetupStatusUpdate()
                    {
                        Date = DateTime.Now,
                        Description = !string.IsNullOrWhiteSpace(request.TaskUpdate.Description) ? request.TaskUpdate.Description : null,
                        FaultCode = request.TaskUpdate.FaultCode,
                        ReservationDate = request.TaskUpdate.ReservationDateParsed
                    };
                    var newTaskStatus = CustomConverter.GetFaultCodeTaskStatus((FaultCodes)request.TaskUpdate.FaultCode);
                    var shouldSendActivationSMS = false;
                    if (newTaskStatus == TaskStatuses.Completed && (short)newTaskStatus != task.TaskStatus)
                    {
                        shouldSendActivationSMS = true;
                        task.Subscription.State = (short)RadiusR.DB.Enums.CustomerState.Active;
                    }
                    task.TaskStatus = (short)CustomConverter.GetFaultCodeTaskStatus((FaultCodes)request.TaskUpdate.FaultCode);
                    task.CompletionDate = CustomConverter.GetCompletionDate((TaskStatuses)task.TaskStatus);
                    task.CustomerSetupStatusUpdates.Add(statusUpdate);
                    db.SaveChanges();

                    if (shouldSendActivationSMS)
                    {
                        var SMSService = new RadiusR.SMS.SMSService();
                        db.SMSArchives.AddSafely(SMSService.SendSubscriberSMS(task.Subscription, RadiusR.DB.Enums.SMSType.Activation));
                        db.SaveChanges();
                    }

                    _logger.LogInfo(request.Username, task.Subscription.SubscriberNo, "Added rendezvous [ID = {0}].", statusUpdate.ID);
                    return new ParameterlessResponse(user.PasswordHash, request)
                    {
                        ResponseMessage = new ServiceResponse()
                        {
                            ErrorCode = (int)ErrorCodes.Success,
                            ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("Success", CommonResponse.CreateCulture(request.Culture))
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<ParameterlessResponse>(request);
            }
        }

        public ParameterlessResponse UpdateClientLocation(UpdateCustomerLocationRequest request)
        {
            try
            {
                _messageLogger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<ParameterlessResponse>(request);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var currentTask = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(task => task.ID == request.LocationUpdate.TaskNo);
                    if (currentTask == null)
                    {
                        return CommonResponse.InvalidTaskNoResponse<ParameterlessResponse>(user.PasswordHash, request);
                    }
                    if (!currentTask.CanBeUpdated())
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<ParameterlessResponse>(user.PasswordHash, request);
                    }

                    if (currentTask.Subscription.SubscriptionGPSCoord == null)
                    {
                        currentTask.Subscription.SubscriptionGPSCoord = new SubscriptionGPSCoord()
                        {
                            Latitude = request.LocationUpdate.Latitude,
                            Longitude = request.LocationUpdate.Longitude
                        };
                    }
                    else
                    {
                        currentTask.Subscription.SubscriptionGPSCoord.Latitude = request.LocationUpdate.Latitude;
                        currentTask.Subscription.SubscriptionGPSCoord.Longitude = request.LocationUpdate.Longitude;
                    }

                    db.SaveChanges();

                    _logger.LogInfo(request.Username, currentTask.Subscription.SubscriberNo, "Updated client GPS coords.");
                    return new ParameterlessResponse(user.PasswordHash, request)
                    {
                        ResponseMessage = new ServiceResponse()
                        {
                            ErrorCode = (int)ErrorCodes.Success,
                            ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("Success", CommonResponse.CreateCulture(request.Culture))
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<ParameterlessResponse>(request);
            }
        }

        public ParameterlessResponse AddCustomerAttachment(AddCustomerAttachmentRequest request)
        {
            try
            {
                _messageLogger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<ParameterlessResponse>(request);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.CustomerAttachment.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.InvalidTaskNoResponse<ParameterlessResponse>(user.PasswordHash, request);
                    }
                    if (!task.CanBeUpdated())
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<ParameterlessResponse>(user.PasswordHash, request);
                    }
                    if (!FileConverter.IsFileTypeAcceptable(request.CustomerAttachment.FileType))
                    {
                        return CommonResponse.InvalidFileTypeResponse<ParameterlessResponse>(user.PasswordHash, request);
                    }
                    if (!FileConverter.IsFileSizeAcceptable(request.CustomerAttachment.FileData))
                    {
                        return CommonResponse.InvalidFileSizeResponse<ParameterlessResponse>(user.PasswordHash, request);
                    }

                    using (Stream tempStream = new MemoryStream())
                    {
                        FileConverter.WriteToStream(tempStream, request.CustomerAttachment.FileData);
                        var fileManager = new MasterISSFileManager();
                        var result = fileManager.SaveClientAttachment(task.SubscriptionID, new FileManagerClientAttachmentWithContent(tempStream, ClientAttachmentTypes.Others, request.CustomerAttachment.FileType.ToLower()));
                        if (result.InternalException != null)
                        {
                            _logger.LogException(request.Username, result.InternalException);
                            return CommonResponse.InternalServerErrorResponse<ParameterlessResponse>(request);
                        }
                    }

                    _logger.LogInfo(request.Username, task.Subscription.SubscriberNo, "Added attachment.");
                    return new ParameterlessResponse(user.PasswordHash, request)
                    {
                        ResponseMessage = new ServiceResponse()
                        {
                            ErrorCode = (int)ErrorCodes.Success,
                            ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("Success", CommonResponse.CreateCulture(request.Culture))
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<ParameterlessResponse>(request);
            }
        }

        public GetCustomerContractResponse GetCustomerContract(TaskNoRequest request)
        {
            try
            {
                _messageLogger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<GetCustomerContractResponse>(request);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.InvalidTaskNoResponse<GetCustomerContractResponse>(user.PasswordHash, request);
                    }
                    using (var fileStream = PDFWriter.GetContractPDF(db, task.SubscriptionID, System.Globalization.CultureInfo.CreateSpecificCulture(request.Culture)))
                    {
                        //var fileColde = FileConverter.GetFileCode(fileStream);
                        _logger.LogInfo(request.Username, task.Subscription.SubscriberNo, "Sent client contract.");
                        return new GetCustomerContractResponse(user.PasswordHash, request)
                        {
                            ResponseMessage = new ServiceResponse()
                            {
                                ErrorCode = (int)ErrorCodes.Success,
                                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("Success", CommonResponse.CreateCulture(request.Culture))
                            },
                            CustomerContract = new CustomerContract()
                            {
                                FileName = task.Subscription.SubscriberNo + "_Contract.pdf",
                                FileCode = FileConverter.GetFileCode(fileStream.Result)
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<GetCustomerContractResponse>(request);
            }
        }
    }
}
