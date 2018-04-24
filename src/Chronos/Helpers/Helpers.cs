using Google.Apis.Calendar.v3.Data;
using System;

namespace Chronos.Helpers
{
    public static class Helpers
    {
        public static string TimePeriodToString(TimePeriod period)
        {
            var start = DateTime.Parse(period.Start.ToString());
            var end = DateTime.Parse(period.End.ToString());
            return start.ToShortTimeString() + " " + start.Date.ToShortDateString()
                + " - " + end.ToShortTimeString() + " " + end.Date.ToShortDateString();
        }
    }
}