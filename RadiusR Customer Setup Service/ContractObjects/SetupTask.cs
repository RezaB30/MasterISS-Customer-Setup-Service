using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.ContractObjects
{
    [DataContract]
    public class SetupTask
    {
        [DataMember]
        public long TaskNo { get; set; }

        [DataMember]
        public string CustomerNo { get; set; }

        [DataMember]
        public string SubscriberNo { get; set; }

        [DataMember]
        public string Province { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string BBK { get; set; }

        [DataMember]
        public string XDSLNo { get; set; }

        [DataMember]
        public string PSTN { get; set; }

        [DataMember]
        public bool HasModem { get; set; }

        [DataMember]
        public string ModemName { get; set; }

        [DataMember]
        public short XDSLType { get; set; }

        [DataMember]
        public string CustomerPhoneNo { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public short CustomerType { get; set; }

        [DataMember]
        public string ReservationDate { get; set; }

        [DataMember]
        public string TaskIssueDate { get; set; }

        [DataMember]
        public string LastConnectionDate { get; set; }

        [DataMember]
        public short TaskType { get; set; }

        [DataMember]
        public short TaskStatus { get; set; }

        [DataMember]
        public string Details { get; set; }
    }
}