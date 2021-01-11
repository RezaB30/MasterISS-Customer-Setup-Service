using RezaB.API.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Requests
{
    [DataContract]
    public class TaskNoRequest : BaseRequest<long, SHA256>
    {
        public long TaskNo
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