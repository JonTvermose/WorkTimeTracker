using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkTimeTracker.Helpers;
using WorkTimeTracker.Models;

namespace WorkTimeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private string _ThisWeekTotalTime;
        private string _TodayTime;
        private string _ThisWeekRemainingTime;
        private string _Week;


        public string ThisWeekTotalTime { 
            get => _ThisWeekTotalTime;
            set
            {
                _ThisWeekTotalTime = value;
                OnPropertyChanged();
            } 
        }

        public string TodayTime
        {
            get => _TodayTime;
            set
            {
                _TodayTime = value;
                OnPropertyChanged();
            }
        }

        public string ThisWeekRemainingTime
        {
            get => _ThisWeekRemainingTime;
            set
            {
                _ThisWeekRemainingTime = value;
                OnPropertyChanged();
            }
        }

        public string Week
        {
            get => _Week;
            set
            {
                _Week = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void UpdateData(TimeSpan weekTime, Day today)
        {
            var todayWorkTime = new TimeSpan(DateTime.UtcNow.TimeOfDay.Ticks - today.StartTime.Value.Ticks);
            ThisWeekTotalTime = (weekTime + todayWorkTime).ToPrettyTime();
            ThisWeekRemainingTime = new TimeSpan(TimeSpan.FromHours(37).Ticks - weekTime.Ticks - todayWorkTime.Ticks).ToPrettyTime();
            TodayTime = todayWorkTime.ToPrettyTime();
            Week = new DateTime(today.DateTicks).GetIso8601WeekOfYear().ToString();
        }

        private void ShowDays_Click(object sender, RoutedEventArgs e)
        {
            var daysList = new DayListWindow();
            daysList.Show();
            this.Close();
        }

        private void ShowWeeks_Click(object sender, RoutedEventArgs e)
        {
            var weekList = new WeekListWindow();
            weekList.Show();
            this.Close();
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
