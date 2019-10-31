using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker.Models
{
    public class Day
    {
        public int Id { get; set; }
        public long DateTicks { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool Holiday { get; set; }

        public Day()
        {
        }

        public Day(DayViewModel dayViewModel)
        {
            Id = dayViewModel.Id;
            DateTicks = TimeZoneInfo.ConvertTimeToUtc(dayViewModel.Date).Ticks;
            StartTime = dayViewModel.StartTime.HasValue && dayViewModel.StartTime.Value.TotalSeconds != 0 ? (TimeSpan?) TimeZoneInfo.ConvertTimeToUtc(new DateTime(dayViewModel.StartTime.Value.Ticks), TimeZoneInfo.Local).TimeOfDay : null;
            EndTime = dayViewModel.EndTime.HasValue && dayViewModel.EndTime.Value.TotalSeconds != 0 ? (TimeSpan?)TimeZoneInfo.ConvertTimeToUtc(new DateTime(dayViewModel.EndTime.Value.Ticks), TimeZoneInfo.Local).TimeOfDay : null;
            Holiday = dayViewModel.Holiday;
        }

        public void Update(DayViewModel dayViewModel)
        {
            DateTicks = TimeZoneInfo.ConvertTimeToUtc(dayViewModel.Date).Ticks;
            StartTime = dayViewModel.StartTime.HasValue && dayViewModel.StartTime.Value.TotalSeconds != 0 ? (TimeSpan?)TimeZoneInfo.ConvertTimeToUtc(new DateTime(dayViewModel.StartTime.Value.Ticks), TimeZoneInfo.Local).TimeOfDay : null;
            EndTime = dayViewModel.EndTime.HasValue && dayViewModel.EndTime.Value.TotalSeconds != 0 ? (TimeSpan?)TimeZoneInfo.ConvertTimeToUtc(new DateTime(dayViewModel.EndTime.Value.Ticks), TimeZoneInfo.Local).TimeOfDay : null;
            Holiday = dayViewModel.Holiday;
        }
    }
}
