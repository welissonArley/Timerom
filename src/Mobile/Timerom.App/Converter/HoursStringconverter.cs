using System;
using System.Globalization;
using Xamarin.Forms;

namespace Timerom.App.Converter
{
    public class HoursStringconverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (TimeSpan)value;

            var hours = time.Hours + ((int)time.TotalDays) * 24;

            return $"{(hours <= 9 ? $"0{hours}" : $"{hours}")}h {(time.Minutes <= 9 ? $"0{time.Minutes}" : $"{time.Minutes}")}m";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
