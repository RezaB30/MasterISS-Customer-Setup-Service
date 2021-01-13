using RadiusR_Customer_Setup_Service.Responses.Parameters;
using RezaB.API.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Responses
{
    [DataContract]
    public class GetCustomerSessionInfoResponse : BaseResponse<CustomerSessionBundle, SHA256>
    {
        public GetCustomerSessionInfoResponse(string passwordHash, BaseRequest<SHA256> baseRequest) : base(passwordHash, baseRequest) { }

        [DataMember]
        public CustomerSessionBundle CustomerSessionBundle
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