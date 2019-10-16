using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeTracker.Helpers
{
    public static class TimeSpanExtensions
    {
        public static string ToPrettyTime(this TimeSpan timespan)
        {
            var hours = (int)timespan.TotalHours;
            var minutes = (int)timespan.TotalMinutes - (60 * hours);
            return $"{hours}h {minutes}m";
        }
    }
}
