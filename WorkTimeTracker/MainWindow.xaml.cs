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

namespace WorkTimeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private string _LastWorkDayTime;
        private string _ThisWeekTotalTime;
        private string _TodayTime;
        private string _ThisWeekRemainingTime;

        public string LastWorkDayTime
        {
            get => _LastWorkDayTime;
            set
            {
                _LastWorkDayTime = value;
                OnPropertyChanged();
            }
        }

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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void UpdateData()
        {

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
