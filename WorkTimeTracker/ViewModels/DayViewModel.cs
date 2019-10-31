using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimeTracker.Models;

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

        public DayViewModel(){}

        public DayViewModel(Day day)
        {
            Id = day.Id;
            Date = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(day.DateTicks), TimeZoneInfo.Local);
            StartTime = day.StartTime.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(new DateTime(day.StartTime.Value.Ticks), TimeZoneInfo.Local).TimeOfDay : TimeSpan.FromSeconds(0);
            EndTime = day.EndTime.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(new DateTime(day.EndTime.Value.Ticks), TimeZoneInfo.Local).TimeOfDay : TimeSpan.FromSeconds(0);
            Holiday = day.Holiday;
        }

    }
}
