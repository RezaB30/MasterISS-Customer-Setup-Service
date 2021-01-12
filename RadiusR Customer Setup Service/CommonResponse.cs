using RadiusR_Customer_Setup_Service.Enums;
using RadiusR_Customer_Setup_Service.Responses;
using RezaB.API.WebService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RadiusR_Customer_Setup_Service
{
    public static class CommonResponse
    {
        public static T UnauthorizedResponse<T>(BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { string.Empty, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.AuthenticationFailed,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("AuthenticationFailed", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T InvalidStartOrEndDateResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.InvalidStartOrEndDate,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidStartOrEndDate", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T InvalidTimespanResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.InvalidTimespan,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidTimespan", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T InvalidTaskStatusResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.InvalidTaskStatus,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidTaskStatus", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T InternalServerErrorResponse<T>(BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { string.Empty, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InternalServerError", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T TaskNotFoundErrorResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.TaskNotFound,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("TaskNotFound", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T MissingCustomerDataResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.MissingCustomerData,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("MissingCustomerData", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T ExternalWebServiceErrorResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.ExternalWebServiceError,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("ExternalWebServiceError", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T InvalidReservationDateResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.InvalidReservationDate,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidReservationDate", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T InvalidFaultCodeResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.InvalidFaultCode,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidFaultCode", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T InvalidTaskNoResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.InvalidTaskNo,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidTaskNo", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T InvalidTaskTypeResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.InvalidTaskType,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidTaskType", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T UnchangeableTaskErrorResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.UnchangeableTaskError,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("UnchangeableTaskError", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T InvalidFileTypeResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.InvalidFileType,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidFileType", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T InvalidFileSizeResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.InvalidFileSize,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidFileSize", CreateCulture(request.Culture))
            };

            return response;
        }

        public static T DescriptionIsTooLongResponse<T>(string passwordHash, BaseRequest<SHA256> request) where T : BaseResponse<SHA256>
        {
            var response = (T)Activator.CreateInstance(typeof(T), new object[] { passwordHash, request });
            response.ResponseMessage = new ServiceResponse()
            {
                ErrorCode = (int)ErrorCodes.DescriptionIsTooLong,
                ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("DescriptionIsTooLong", CreateCulture(request.Culture))
            };

            return response;
        }

        public static CultureInfo CreateCulture(string cultureName)
        {
            var currentCulture = CultureInfo.InvariantCulture;
            try
            {
                currentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            }
            catch { }
            return currentCulture;
        }
    }
}