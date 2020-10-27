using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Requests
{
    [DataContract]
    public class AddCustomerAttachmentRequest : CustomerSetupServiceRequestBase
    {
        [DataMember]
        public override string Culture { get; set; }

        [DataMember]
        public override string Hash { get; set; }

        [DataMember]
        public override string Username { get; set; }

        [DataMember]
        public long TaskNo { get; set; }

        [DataMember]
        public string FileData { get; set; }

        [DataMember]
        public string FileType { get; set; }
    }
}