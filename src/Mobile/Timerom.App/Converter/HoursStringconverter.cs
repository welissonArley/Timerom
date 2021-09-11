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

            return $"{(time.Hours <= 9 ? $"0{time.Hours}" : $"{time.Hours}")}h {(time.Minutes <= 9 ? $"0{time.Minutes}" : $"{time.Minutes}")}m";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
