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
        private List<DayViewModel> _DayList;

        public List<DayViewModel> DayList
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
                var days = dbContext.Days.OrderByDescending(x => x.DateTicks).Take(100).ToList().Select(x => new DayViewModel
                {
                    Date = new DateTime(x.DateTicks),
                    EndTime = x.EndTime,
                    Holiday = x.Holiday,
                    StartTime = x.StartTime
                }).ToList();
                Show(days);
            }
        }

        public void Show(List<DayViewModel> days)
        {
            DayList = days;
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
