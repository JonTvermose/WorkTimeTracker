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
using WorkTimeTracker.Models;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker
{
    /// <summary>
    /// Interaction logic for DayListWindow.xaml
    /// </summary>
    public partial class DayListWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<DayViewModel> _DayList;

        public ObservableCollection<DayViewModel> DayList
        {
            get => _DayList;
            set
            {
                _DayList = value;
                OnPropertyChanged();
            }
        }

        public DayListWindow()
        {
            InitializeComponent();
            DataContext = this;
            using (var dbContext = new ApplicationDbContext())
            {
                var days = dbContext.Days.OrderByDescending(x => x.DateTicks).ToList().Select(x => new DayViewModel(x)).ToList();
                Show(days);
            }
        }

        public void Show(List<DayViewModel> days)
        {
            DayList = new ObservableCollection<DayViewModel>(days);
        }

        private void CheckboxChecked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            var item = cb.DataContext as DayViewModel;
            item.Holiday = true;
            using (var dbContext = new ApplicationDbContext())
            {
                var day = dbContext.Days.SingleOrDefault(x => x.Id == item.Id);
                day.Holiday = item.Holiday;
                dbContext.SaveChanges();
            }
        }

        private void CheckboxUnchecked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            var item = cb.DataContext as DayViewModel;
            using (var dbContext = new ApplicationDbContext())
            {
                var day = dbContext.Days.Single(x => x.Id == item.Id);
                day.Holiday = item.Holiday;
                dbContext.SaveChanges();
            }
        }

        private void CreateDay_Click(object sender, RoutedEventArgs e)
        {
            var latestDay = DayList.OrderByDescending(x => x.Date).First();
            var day = new Day
            {
                DateTicks = TimeZoneInfo.ConvertTimeToUtc(latestDay.Date.AddDays(1)).Ticks
            };
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Days.Add(day);
                dbContext.SaveChanges();
            }
            DayList.Add(new DayViewModel(day));
            DayList = new ObservableCollection<DayViewModel>(DayList.OrderByDescending(x => x.Date));
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveButton.Content = "Saving...";
                using (var dbContext = new ApplicationDbContext())
                {
                    var days = dbContext.Days.ToList();
                    foreach (var day in days)
                    {
                        var changedDay = DayList.Single(x => TimeZoneInfo.ConvertTimeToUtc(x.Date, TimeZoneInfo.Local).Ticks == day.DateTicks);
                        day.Update(changedDay);
                    }
                    dbContext.SaveChanges();
                }
                SaveButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#10ac84"));
                SaveButton.Content = "Done!";
                Task.Run(async () =>
                {
                    await Task.Delay(3000);
                    await SaveButton.Dispatcher.BeginInvoke((Action)(() => {
                        SaveButton.Content = "Save changes";
                        SaveButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222f3e"));
                    }));
                });
            } catch (Exception ex)
            {
                SaveButton.Content = $"Error! {ex.Message}";
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
