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
                return ServiceTypeConverter.ParseDate(StartDate);
            }
            set
            {
                if (value.HasValue)
                    StartDate = ServiceTypeConverter.GetDateString(value.Value);
                else
                    StartDate = null;
            }
        }

        public DateTime? EndDateParsed
        {
            get
            {
                return ServiceTypeConverter.ParseDate(EndDate);
            }
            set
            {
                if (value.HasValue)
                    EndDate = ServiceTypeConverter.GetDateString(value.Value);
                else
                    EndDate = null;
            }
        }
    }
}