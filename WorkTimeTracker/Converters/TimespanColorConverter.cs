using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WorkTimeTracker
{
    public class TimespanColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = value as double?;
            if(time != null)
            {
                var red = (byte) (2.0f * time.Value);
                var green = (byte) (2.0f * (1 - time.Value));
                var brush = new SolidColorBrush(Color.FromRgb(red, green, 0));
                return brush;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
