using RezaB.API.WebService.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Responses.Parameters
{
    [DataContract]
    public class SavedTaskUpdate
    {
        [DataMember]
        public short FaultCode { get; set; }

        [DataMember]
        public string CreationDate { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ReservationDate { get; set; }

        public DateTime? CreationDateParsed
        {
            get
            {
                return ServiceTypeConverter.ParseDateTime(CreationDate);
            }
            set
            {
                if (value.HasValue)
                    CreationDate = ServiceTypeConverter.GetDateTimeString(value.Value);
                else
                    CreationDate = null;
            }
        }

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