using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using RezaB.API.WebService.DataTypes;

namespace RadiusR_Customer_Setup_Service.Requests.Parameters
{
    [DataContract]
    public class TaskUpdate
    {
        [DataMember]
        public long TaskNo { get; set; }

        [DataMember]
        public short FaultCode { get; set; }

        [DataMember]
        public string ReservationDate { get; set; }

        [DataMember]
        public string Description { get; set; }

        public DateTime? ReservationDateParsed
        {
            get
            {
                return ServiceTypeConverter.ParseDateTime(ReservationDate);
            }
            set
            {
                if (value.HasValue)
                    ReservationDate = ServiceTypeConverter.GetDateTimeString(value.Value);
                else
                    ReservationDate = null;
            }
        }
    }
}