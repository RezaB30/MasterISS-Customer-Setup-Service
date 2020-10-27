using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Responses
{
    [DataContract]
    public class GetCustomerLineDetailsResponse : CustomerSetupServiceResponseBase
    {
        [DataMember]
        public override int ErrorCode { get; set; }

        [DataMember]
        public override string ErrorMessage { get; set; }

        [DataMember]
        public string XDSLNo { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public string DownloadNoiseMargin { get; set; }

        [DataMember]
        public string UploadNoiseMargin { get; set; }

        [DataMember]
        public string CurrentDownloadSpeed { get; set; }

        [DataMember]
        public string CurrentUploadSpeed { get; set; }

        [DataMember]
        public string DownloadSpeedCapacity { get; set; }

        [DataMember]
        public string UploadSpeedCapacity { get; set; }
    }
}