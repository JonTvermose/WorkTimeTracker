using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Helpers
{
    public static class DayExtensions
    {
        public static TimeSpan WorkTime(this Day day)
        {
            if(day.EndTime.HasValue && day.StartTime.HasValue)
            {
                return new TimeSpan(day.EndTime.Value.Ticks - day.StartTime.Value.Ticks);
            }
            return TimeSpan.FromTicks(0);
        }
    }
}
