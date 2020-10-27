using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Responses
{
    [DataContract]
    public abstract class CustomerSetupServiceResponseBase
    {
        [DataMember]
        public abstract int ErrorCode { get; set; }

        [DataMember]
        public abstract string ErrorMessage { get; set; }
    }
}