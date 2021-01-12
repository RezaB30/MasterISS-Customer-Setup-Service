using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Requests.Parameters
{
    [DataContract]
    public class CustomerAttachment
    {
        [DataMember]
        public long TaskNo { get; set; }

        [DataMember]
        public short AttachmentType { get; set; }

        [DataMember]
        public string FileData { get; set; }

        [DataMember]
        public string FileType { get; set; }
    }
}