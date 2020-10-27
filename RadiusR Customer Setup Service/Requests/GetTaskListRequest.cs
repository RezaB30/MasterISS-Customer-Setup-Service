using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RadiusR_Customer_Setup_Service.Requests
{
    [DataContract]
    public class GetTaskListRequest : CustomerSetupServiceRequestBase
    {
        [DataMember]
        public override string Hash { get; set; }

        [DataMember]
        public override string Username { get; set; }

        [DataMember]
        public override string Culture { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        public DateTime? _startDate
        {
            get
            {
                return DataBinder.ParseDateTime(StartDate);
            }
            set
            {
                StartDate = DataBinder.GetDateTimeString(value);
            }
        }

        public DateTime? _endDate
        {
            get
            {
                return DataBinder.ParseDateTime(EndDate);
            }
            set
            {
                EndDate = DataBinder.GetDateTimeString(value);
            }
        }
    }
}