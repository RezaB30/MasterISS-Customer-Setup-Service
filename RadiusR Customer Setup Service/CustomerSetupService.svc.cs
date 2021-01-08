using RadiusR_Customer_Setup_Service.Authentication;
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
using RadiusR_Customer_Setup_Service.ContractObjects;
using System.Data.Entity;
using RadiusR.Services.NLog;
using RezaB.TurkTelekom.WebServices.TTOYS;
using System.IO;
using RadiusR.Files;
using RadiusR.DB.Enums.CustomerSetup;
using RadiusR.PDFForms;
using RadiusR.DB.DomainsCache;
using RadiusR.DB.ModelExtentions;
using RadiusR.FileManagement;

namespace RadiusR_Customer_Setup_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class CustomerSetupService : ICustomerSetupService
    {
        private WebServiceLogger _logger = new WebServiceLogger("main");

        public string GetKeyFragment(string username)
        {
            return KeyManager.GenerateKey(username);
        }

        public GetTaskListResponse GetTaskList(GetTaskListRequest request)
        {
            try
            {
                _logger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<GetTaskListResponse>(request.Culture);
                }
                if (request._startDate == null || request._endDate == null)
                {
                    return CommonResponse.InvalidStartOrEndDateResponse<GetTaskListResponse>(request.Culture);
                }
                if (request._endDate - request._startDate > TimeSpan.FromDays(30))
                {
                    return CommonResponse.InvalidTimespanResponse<GetTaskListResponse>(request.Culture);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var tasks = db.CustomerSetupTasks.GetUserTasks(user.UserId).Where(task => task.TaskIssueDate >= request._startDate && DbFunctions.TruncateTime(task.TaskIssueDate) <= request._endDate)
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
                    return new GetTaskListResponse()
                    {
                        ErrorCode = (int)ErrorCodes.Success,
                        TaskList = taskList.ToList()
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<GetTaskListResponse>(request.Culture);
            }
        }

        public GetCustomerCredentialResponse GetCustomerCredentials(TaskNoRequest request)
        {
            try
            {
                _logger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<GetCustomerCredentialResponse>(request.Culture);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.TaskNotFoundErrorResponse<GetCustomerCredentialResponse>(request.Culture);
                    }
                    if (!task.CanBeUpdated())
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<GetCustomerCredentialResponse>(request.Culture);
                    }

                    _logger.LogInfo(request.Username, task.Subscription.SubscriberNo);
                    return new GetCustomerCredentialResponse()
                    {
                        ErrorCode = (int)ErrorCodes.Success,
                        Username = task.Subscription.Username,
                        Password = task.Subscription.RadiusPassword
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<GetCustomerCredentialResponse>(request.Culture);
            }
        }

        public GetCustomerLineDetailsResponse GetCustomerLineDetails(TaskNoRequest request)
        {
            try
            {
                _logger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<GetCustomerLineDetailsResponse>(request.Culture);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.TaskNotFoundErrorResponse<GetCustomerLineDetailsResponse>(request.Culture);
                    }
                    if (!task.CanBeUpdated())
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<GetCustomerLineDetailsResponse>(request.Culture);
                    }
                    var cachedDomain = DomainsCache.GetDomainByID(task.Subscription.DomainID);
                    if (task.Subscription.SubscriptionTelekomInfo == null || cachedDomain.TelekomCredential == null)
                    {
                        return CommonResponse.MissingCustomerDataResponse<GetCustomerLineDetailsResponse>(request.Culture);
                    }

                    TTOYSServiceClient externalClient = new TTOYSServiceClient(cachedDomain.TelekomCredential.XDSLWebServiceUsernameInt, cachedDomain.TelekomCredential.XDSLWebServicePassword);
                    var response = externalClient.Check(task.Subscription.SubscriptionTelekomInfo.SubscriptionNo);
                    if (response.InternalException != null)
                    {
                        return CommonResponse.ExternalWebServiceErrorResponse<GetCustomerLineDetailsResponse>(request.Culture);
                    }

                    _logger.LogInfo(request.Username, task.Subscription.SubscriberNo);
                    return new GetCustomerLineDetailsResponse()
                    {
                        ErrorCode = (int)ErrorCodes.Success,
                        XDSLNo = task.Subscription.SubscriptionTelekomInfo.SubscriptionNo,
                        IsActive = response.Data.OperationStatus == TTOYSServiceClient.OperationStatus.Open,
                        DownloadNoiseMargin = response.Data.NoiseRateDown,
                        UploadNoiseMargin = response.Data.NoiseRateUp,
                        CurrentDownloadSpeed = response.Data.CurrentDown,
                        CurrentUploadSpeed = response.Data.CurrentUp,
                        DownloadSpeedCapacity = response.Data.MaxDown,
                        UploadSpeedCapacity = response.Data.MaxUp
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<GetCustomerLineDetailsResponse>(request.Culture);
            }
        }

        public GetCustomerSessionInfoResponse GetCustomerSessionInfo(TaskNoRequest request)
        {
            try
            {
                _logger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<GetCustomerSessionInfoResponse>(request.Culture);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.TaskNotFoundErrorResponse<GetCustomerSessionInfoResponse>(request.Culture);
                    }
                    if (!task.CanBeUpdated())
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<GetCustomerSessionInfoResponse>(request.Culture);
                    }

                    var firstSession = task.Subscription.RadiusAccountings.OrderBy(ra => ra.StartTime).FirstOrDefault();
                    _logger.LogInfo(request.Username, task.Subscription.SubscriberNo);
                    if (firstSession == null)
                    {
                        return new GetCustomerSessionInfoResponse()
                        {
                            ErrorCode = (int)ErrorCodes.Success,
                            IsOnline = false
                        };
                    }

                    return new GetCustomerSessionInfoResponse()
                    {
                        ErrorCode = (int)ErrorCodes.Success,
                        IsOnline = !firstSession.StopTime.HasValue,
                        NASIPAddress = firstSession.NASIP,
                        SessionId = firstSession.SessionID,
                        SessionTime = TimeSpan.FromSeconds(firstSession.SessionTime).ToString(@"dd\.hh\:mm\:ss"),
                        SessionStart = DataBinder.GetDateTimeString(firstSession.StartTime),
                        IPAddress = firstSession.RadiusAccountingIPInfo != null ? firstSession.RadiusAccountingIPInfo.RealIP : null
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<GetCustomerSessionInfoResponse>(request.Culture);
            }
        }

        public BasicResponse AddTaskStatusUpdate(AddTaskStatusUpdateRequest request)
        {
            try
            {
                _logger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<BasicResponse>(request.Culture);
                }
                if (!string.IsNullOrEmpty(request.ReservationDate) && !request._ReservationDate.HasValue)
                {
                    return CommonResponse.InvalidReservationDateResponse<BasicResponse>(request.Culture);
                }
                if (!Enum.IsDefined(typeof(FaultCodes), (int)request.FaultCode))
                {
                    return CommonResponse.InvalidFaultCodeResponse<BasicResponse>(request.Culture);
                }
                if (request.Description != null && request.Description.Length > 450)
                {
                    return CommonResponse.DescriptionIsTooLongResponse<BasicResponse>(request.Culture);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.InvalidTaskNoResponse<BasicResponse>(request.Culture);
                    }
                    if (!task.CanChangeState((FaultCodes)request.FaultCode))
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<BasicResponse>(request.Culture);
                    }

                    var statusUpdate = new CustomerSetupStatusUpdate()
                    {
                        Date = DateTime.Now,
                        Description = !string.IsNullOrWhiteSpace(request.Description) ? request.Description : null,
                        FaultCode = request.FaultCode,
                        ReservationDate = request._ReservationDate
                    };
                    var newTaskStatus = CustomConverter.GetFaultCodeTaskStatus((FaultCodes)request.FaultCode);
                    var shouldSendActivationSMS = false;
                    if (newTaskStatus == TaskStatuses.Completed && (short)newTaskStatus != task.TaskStatus)
                    {
                        shouldSendActivationSMS = true;
                        task.Subscription.State = (short)RadiusR.DB.Enums.CustomerState.Active;
                    }
                    task.TaskStatus = (short)CustomConverter.GetFaultCodeTaskStatus((FaultCodes)request.FaultCode);
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
                    return new BasicResponse()
                    {
                        ErrorCode = (int)ErrorCodes.Success
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<BasicResponse>(request.Culture);
            }
        }

        public BasicResponse UpdateClientLocation(UpdateCustomerLocationRequest request)
        {
            try
            {
                _logger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<BasicResponse>(request.Culture);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var currentTask = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(task => task.ID == request.TaskNo);
                    if (currentTask == null)
                    {
                        return CommonResponse.InvalidTaskNoResponse<BasicResponse>(request.Culture);
                    }
                    if (!currentTask.CanBeUpdated())
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<BasicResponse>(request.Culture);
                    }

                    if (currentTask.Subscription.SubscriptionGPSCoord == null)
                    {
                        currentTask.Subscription.SubscriptionGPSCoord = new SubscriptionGPSCoord()
                        {
                            Latitude = request.Latitude,
                            Longitude = request.Longitude
                        };
                    }
                    else
                    {
                        currentTask.Subscription.SubscriptionGPSCoord.Latitude = request.Latitude;
                        currentTask.Subscription.SubscriptionGPSCoord.Longitude = request.Longitude;
                    }

                    db.SaveChanges();

                    _logger.LogInfo(request.Username, currentTask.Subscription.SubscriberNo, "Updated client GPS coords.");
                    return new BasicResponse()
                    {
                        ErrorCode = (int)ErrorCodes.Success
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<BasicResponse>(request.Culture);
            }
        }

        public BasicResponse AddCustomerAttachment(AddCustomerAttachmentRequest request)
        {
            try
            {
                _logger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<BasicResponse>(request.Culture);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.InvalidTaskNoResponse<BasicResponse>(request.Culture);
                    }
                    if (!task.CanBeUpdated())
                    {
                        return CommonResponse.UnchangeableTaskErrorResponse<BasicResponse>(request.Culture);
                    }
                    if (!FileConverter.IsFileTypeAcceptable(request.FileType))
                    {
                        return CommonResponse.InvalidFileTypeResponse<BasicResponse>(request.Culture);
                    }
                    if (!FileConverter.IsFileSizeAcceptable(request.FileData))
                    {
                        return CommonResponse.InvalidFileSizeResponse<BasicResponse>(request.Culture);
                    }

                    using (Stream tempStream = new MemoryStream())
                    {
                        FileConverter.WriteToStream(tempStream, request.FileData);
                        var fileManager = new MasterISSFileManager();
                        var result = fileManager.SaveClientAttachment(task.SubscriptionID, new FileManagerClientAttachmentWithContent(tempStream, ClientAttachmentTypes.Others, request.FileType.ToLower()));
                        if (result.InternalException != null)
                        {
                            _logger.LogException(request.Username, result.InternalException);
                            return CommonResponse.InternalServerErrorResponse<BasicResponse>(request.Culture);
                        }
                    }

                    _logger.LogInfo(request.Username, task.Subscription.SubscriberNo, "Added attachment.");
                    return new BasicResponse()
                    {
                        ErrorCode = (int)ErrorCodes.Success
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<BasicResponse>(request.Culture);
            }
        }

        public GetCustomerContractResponse GetCustomerContract(TaskNoRequest request)
        {
            try
            {
                _logger.LogIncomingMessage(request);

                var user = Authenticator.Authenticate(request);
                if (user == null)
                {
                    return CommonResponse.UnauthorizedResponse<GetCustomerContractResponse>(request.Culture);
                }
                using (RadiusREntities db = new RadiusREntities())
                {
                    var task = db.CustomerSetupTasks.GetUserTasks(user.UserId).FirstOrDefault(t => t.ID == request.TaskNo);
                    if (task == null)
                    {
                        return CommonResponse.InvalidTaskNoResponse<GetCustomerContractResponse>(request.Culture);
                    }
                    using (var fileStream = PDFWriter.GetContractPDF(db, task.SubscriptionID, System.Globalization.CultureInfo.CreateSpecificCulture(request.Culture)))
                    {
                        //var fileColde = FileConverter.GetFileCode(fileStream);
                        _logger.LogInfo(request.Username, task.Subscription.SubscriberNo, "Sent client contract.");
                        return new GetCustomerContractResponse()
                        {
                            ErrorCode = (int)ErrorCodes.Success,
                            FileName = task.Subscription.SubscriberNo + "_Contract.pdf",
                            FileCode = FileConverter.GetFileCode(fileStream.Result)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.Username, ex);
                return CommonResponse.InternalServerErrorResponse<GetCustomerContractResponse>(request.Culture);
            }
        }
    }
}
