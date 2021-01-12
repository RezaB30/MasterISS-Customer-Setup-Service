using RadiusR_Customer_Setup_Service.Requests.Parameters;
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
    public class UpdateCustomerLocationRequest : BaseRequest<LocationUpdate, SHA256>
    {
        [DataMember]
        public LocationUpdate LocationUpdate
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