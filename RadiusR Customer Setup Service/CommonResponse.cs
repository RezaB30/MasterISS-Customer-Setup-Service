using RadiusR_Customer_Setup_Service.Enums;
using RadiusR_Customer_Setup_Service.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RadiusR_Customer_Setup_Service
{
    public static class CommonResponse
    {
        public static T UnauthorizedResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.AuthenticationFailed;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("AuthenticationFailed", CreateCulture(culture));
            return response;
        }

        public static T InvalidStartOrEndDateResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.InvalidStartOrEndDate;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidStartOrEndDate", CreateCulture(culture));
            return response;
        }

        public static T InvalidTimespanResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.InvalidTimespan;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidTimespan", CreateCulture(culture));
            return response;
        }

        public static T InvalidTaskStatusResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.InvalidTaskStatus;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidTaskStatus", CreateCulture(culture));
            return response;
        }

        public static T InternalServerErrorResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.InternalServerError;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InternalServerError", CreateCulture(culture));
            return response;
        }

        public static T TaskNotFoundErrorResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.TaskNotFound;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("TaskNotFound", CreateCulture(culture));
            return response;
        }

        public static T MissingCustomerDataResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.MissingCustomerData;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("MissingCustomerData", CreateCulture(culture));
            return response;
        }

        public static T ExternalWebServiceErrorResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.ExternalWebServiceError;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("ExternalWebServiceError", CreateCulture(culture));
            return response;
        }

        public static T InvalidReservationDateResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.InvalidReservationDate;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidReservationDate", CreateCulture(culture));
            return response;
        }

        public static T InvalidFaultCodeResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.InvalidFaultCode;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidFaultCode", CreateCulture(culture));
            return response;
        }

        public static T InvalidTaskNoResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.InvalidTaskNo;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidTaskNo", CreateCulture(culture));
            return response;
        }

        public static T InvalidTaskTypeResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.InvalidTaskType;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidTaskType", CreateCulture(culture));
            return response;
        }

        public static T UnchangeableTaskErrorResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.UnchangeableTaskError;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("UnchangeableTaskError", CreateCulture(culture));
            return response;
        }

        public static T InvalidFileTypeResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.InvalidFileType;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidFileType", CreateCulture(culture));
            return response;
        }

        public static T InvalidFileSizeResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.InvalidFileSize;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("InvalidFileSize", CreateCulture(culture));
            return response;
        }

        public static T DescriptionIsTooLongResponse<T>(string culture) where T : CustomerSetupServiceResponseBase, new()
        {
            var response = new T();
            response.ErrorCode = (int)ErrorCodes.DescriptionIsTooLong;
            response.ErrorMessage = Localization.ErrorMessages.ResourceManager.GetString("DescriptionIsTooLong", CreateCulture(culture));
            return response;
        }

        private static CultureInfo CreateCulture(string cultureName)
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