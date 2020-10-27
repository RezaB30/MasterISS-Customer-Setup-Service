using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Requests
{
    [DataContract]
    public abstract class CustomerSetupServiceRequestBase
    {
        [DataMember]
        public abstract string Username { get; set; }

        [DataMember]
        public abstract string Hash { get; set; }

        [DataMember]
        public abstract string Culture { get; set; }
    }
}