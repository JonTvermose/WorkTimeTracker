using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeTracker.ViewModels
{
    public class DayViewModel
    {
        private DateTime _Date;
        public DateTime Date
        {
            get => _Date;
            set
            {
                _Date = TimeZoneInfo.ConvertTimeFromUtc(value, TimeZoneInfo.Local);
            }
        }
        private TimeSpan _StartTime;
        public TimeSpan? StartTime { 
            get => _StartTime; 
            set 
            {
                if(value.HasValue)
                    _StartTime = new DateTime(Date.Ticks, DateTimeKind.Local).Add(value.Value).TimeOfDay;
            }
        }

        private TimeSpan _EndTime;
        public TimeSpan? EndTime
        {
            get => _EndTime;
            set
            {
                if(value.HasValue)
                    _EndTime = new DateTime(Date.Ticks, DateTimeKind.Local).Add(value.Value).TimeOfDay;
            }
        }

        public bool Holiday { get; set; }

    }
}
