using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkTimeTracker.Helpers;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker
{
    /// <summary>
    /// Interaction logic for WeekListWindow.xaml
    /// </summary>
    public partial class WeekListWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<WeekViewModel> _WeekList;
        
        public ObservableCollection<WeekViewModel> WeekList
        {
            get => _WeekList;
            set
            {
                _WeekList = value;
                OnPropertyChanged();
            }
        }
        public WeekListWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            using (var dbContext = new ApplicationDbContext())
            {
                var weeks = dbContext.Days.OrderByDescending(x => x.DateTicks).ToList()
                    .GroupBy(x => new {
                        week = new DateTime(x.DateTicks).GetIso8601WeekOfYear(),
                        year = new DateTime(x.DateTicks).Year
                    }, (key, group) =>
                        new WeekViewModel(
                            key.week,
                            key.year,
                            TimeSpan.FromMinutes(group.Sum(g => g.WorkTime().TotalMinutes)))
                    ).OrderBy(x => x.Year).ThenBy(x => x.Week).ToList();
                var accumulatedDelta = TimeSpan.FromMinutes(0);
                foreach (var week in weeks)
                {
                    week.TotalDelta = accumulatedDelta.Add(week.Delta);
                    accumulatedDelta = week.TotalDelta;
                }
                    
                WeekList = new ObservableCollection<WeekViewModel>(weeks.OrderByDescending(x => x.Year).ThenByDescending(x => x.Week));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}
