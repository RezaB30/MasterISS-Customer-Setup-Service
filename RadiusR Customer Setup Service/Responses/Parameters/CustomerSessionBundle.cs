using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Responses.Parameters
{
    [DataContract]
    public class CustomerSessionBundle
    {
        [DataMember]
        public CustomerSessionInfo FirstSession { get; set; }

        [DataMember]
        public CustomerSessionInfo LastSession { get; set; }
    }
}