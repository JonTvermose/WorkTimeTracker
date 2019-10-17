using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WorkTimeTracker.Helpers;

namespace WorkTimeTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private bool _isExit;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
            _notifyIcon.Icon = WorkTimeTracker.Resources.IconTray;
            _notifyIcon.Visible = true;

            CreateContextMenu();
            ShowMainWindow();
            SystemEvents.SessionEnding += OnShutdown;
        }

        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip =
              new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Work Time Tracker").Click += (s, e) => ShowMainWindow();
            _notifyIcon.ContextMenuStrip.Items.Add("Close").Click += (s, e) => ExitApplication();
        }

        private void ExitApplication()
        {
            _isExit = true;
            MainWindow.Close();
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        public void OnShutdown(object sender, EventArgs e)
        {
            // Register the shutdown time
            using (var context = new ApplicationDbContext())
            {
                var today = context.Days.SingleOrDefault(x => x.DateTicks == DateTime.UtcNow.Date.Ticks);
                if(today != null)
                {
                    today.EndTime = DateTime.UtcNow.TimeOfDay;
                    context.SaveChanges();
                } else
                {
                    today = new Models.Day
                    {
                        DateTicks = DateTime.UtcNow.Date.Ticks,
                        EndTime = DateTime.UtcNow.TimeOfDay
                    };
                    context.Days.Add(today);
                    context.SaveChanges();
                }
            }
        }

        private void UpdateData()
        {
            using (var context = new ApplicationDbContext())
            {
                // TODO Fetch data/calculate and update the data shown by mainwindow
                var todayTicks = DateTime.UtcNow.Date.Ticks;
                var today = context.Days.SingleOrDefault(x => x.DateTicks == todayTicks);
                if (today == null)
                {
                    today = new Models.Day
                    {
                        DateTicks = DateTime.UtcNow.Date.Ticks,
                        StartTime = DateTime.UtcNow.TimeOfDay
                    };
                    context.Days.Add(today);
                    context.SaveChanges();
                }
                var dayOfWeek = new DateTime(today.DateTicks).DayOfWeek; // mon = 1
                var monday = DateTime.UtcNow.Date.AddDays(1 - (int)dayOfWeek);
                var thisWeekDays = context.Days.Where(x => x.DateTicks >= monday.Ticks && x.DateTicks < today.DateTicks).ToList();
                TimeSpan weekTime = TimeSpan.FromSeconds(0);
                foreach(var day in thisWeekDays)
                {
                    weekTime += day.WorkTime();
                }

                ((MainWindow)MainWindow).UpdateData(weekTime, today);
            }
        }

        private void ShowMainWindow()
        {
            UpdateData();

            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                MainWindow.Hide(); // A hidden window can be shown again, a closed one not
            }
        }
    }
}
