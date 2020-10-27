using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Enums
{
    public enum ErrorCodes
    {
        Success = 0,
        AuthenticationFailed = 1,
        InvalidStartOrEndDate = 2,
        InvalidTimespan = 3,
        InvalidTaskStatus = 4,
        TaskNotFound = 5,
        MissingCustomerData = 6,
        ExternalWebServiceError = 7,
        InvalidReservationDate = 8,
        InvalidFaultCode = 9,
        InvalidTaskNo = 10,
        InvalidTaskType = 11,
        UnchangeableTaskError = 12,
        InvalidFileType = 13,
        InvalidFileSize = 14,
        DescriptionIsTooLong = 15,

        InternalServerError = 199
    }
}