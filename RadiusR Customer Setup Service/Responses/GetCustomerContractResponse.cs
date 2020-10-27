using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Responses
{
    [DataContract]
    public class GetCustomerContractResponse : CustomerSetupServiceResponseBase
    {
        [DataMember]
        public override int ErrorCode { get; set; }

        [DataMember]
        public override string ErrorMessage { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string FileCode { get; set; }
    }
}