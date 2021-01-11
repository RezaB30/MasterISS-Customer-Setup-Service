using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Web;
using RadiusR_Customer_Setup_Service.Requests.Parameters;
using RezaB.API.WebService;

namespace RadiusR_Customer_Setup_Service.Requests
{
    [DataContract]
    public class GetTaskListRequest : BaseRequest<DateSpan, SHA256>
    {
        public DateSpan DateSpan
        {
            get
            {
                return Data;
            }
            set
            {
                Data = value;
            }
        }
    }
}