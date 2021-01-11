using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Responses.Parameters
{
    [DataContract]
    public class CustomerSessionInfo
    {
        [DataMember]
        public bool IsOnline { get; set; }

        [DataMember]
        public string SessionId { get; set; }

        [DataMember]
        public string NASIPAddress { get; set; }

        [DataMember]
        public string IPAddress { get; set; }

        [DataMember]
        public string SessionTime { get; set; }

        [DataMember]
        public string SessionStart { get; set; }
    }
}