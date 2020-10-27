using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadiusR_Customer_Setup_Service
{
    public static class DataBinder
    {
        public static DateTime? ParseDate(string date)
        {
            DateTime results;
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out results))
            {
                return results;
            }
            return null;
        }

        public static DateTime? ParseDateTime(string dateTime)
        {
            DateTime results;
            if (DateTime.TryParseExact(dateTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out results))
            {
                return results;
            }
            return null;
        }

        public static string GetDateString(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) : null;
        }

        public static string GetDateTimeString(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : null;
        }
    }
}