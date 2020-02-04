using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeTracker.ViewModels
{
    public class WeekViewModel
    {
        public int Year { get; set; }
        public int Week { get; set; }
        public TimeSpan Total { get; set; }
        public TimeSpan Delta { get; set; }
        public TimeSpan TotalDelta { get; set; }
        public string WeekYear { get; set; }

        public WeekViewModel(int week, int year, TimeSpan total)
        {
            Year = year;
            Week = week;
            WeekYear = week.ToString() + "-" + year.ToString();
            Total = total;
            Delta = TimeSpan.FromHours(-37).Add(Total);
        }

    }
}
