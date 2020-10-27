using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Requests
{
    [DataContract]
    public class UpdateCustomerLocationRequest : CustomerSetupServiceRequestBase
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
        public decimal Latitude { get; set; }

        [DataMember]
        public decimal Longitude { get; set; }
    }
}