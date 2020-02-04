using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimeTracker.Models;
using WorkTimeTracker.Helpers;

namespace WorkTimeTracker.ViewModels
{
    public class DayViewModel
    {
        public int Id { get; set; }

        public DateTime Date
        {
            get; set;
        }

        public TimeSpan? StartTime {
            get; set;
        }

        public TimeSpan? EndTime
        {
            get; set;
        }

        public bool Holiday { get; set; }

        public string TotalTime { get; set; }

        public DayViewModel(){}

        public DayViewModel(Day day)
        {
            Id = day.Id;
            Date = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(day.DateTicks), TimeZoneInfo.Local);
            StartTime = day.StartTime.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(new DateTime(day.StartTime.Value.Ticks), TimeZoneInfo.Local).TimeOfDay : TimeSpan.FromSeconds(0);
            EndTime = day.EndTime.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(new DateTime(day.EndTime.Value.Ticks), TimeZoneInfo.Local).TimeOfDay : TimeSpan.FromSeconds(0);
            Holiday = day.Holiday;
            if(!Holiday && StartTime.HasValue && EndTime.HasValue)
            {
                TotalTime = (EndTime.Value - StartTime.Value).ToPrettyTime();
            } 
            if (Holiday)
            {
                TotalTime = TimeSpan.FromHours(7).Add(TimeSpan.FromMinutes(24)).ToPrettyTime();
            }
        }

    }
}
