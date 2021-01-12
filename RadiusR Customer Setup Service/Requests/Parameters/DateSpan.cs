using RezaB.API.WebService.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Requests.Parameters
{
    [DataContract]
    public class DateSpan
    {
        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        public DateTime? StartDateParsed
        {
            get
            {
                return ServiceTypeConverter.ParseDateTime(StartDate);
            }
            set
            {
                if (value.HasValue)
                    StartDate = ServiceTypeConverter.GetDateTimeString(value.Value);
                else
                    StartDate = null;
            }
        }

        public DateTime? EndDateParsed
        {
            get
            {
                return ServiceTypeConverter.ParseDateTime(EndDate);
            }
            set
            {
                if (value.HasValue)
                    EndDate = ServiceTypeConverter.GetDateTimeString(value.Value);
                else
                    EndDate = null;
            }
        }
    }
}