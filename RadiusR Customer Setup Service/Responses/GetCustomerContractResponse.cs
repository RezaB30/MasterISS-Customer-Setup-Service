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
    public class GetCustomerContractResponse : BaseResponse<CustomerContract, SHA256>
    {
        public GetCustomerContractResponse(string passwordHash, BaseRequest<SHA256> baseRequest) : base(passwordHash, baseRequest) { }

        [DataMember]
        public CustomerContract CustomerContract
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