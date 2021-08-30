using System;
using System.Globalization;
using Xamarin.Forms;

namespace Timerom.App.Converter
{
    public class HoursStringconverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hour = (double)value;

            return $"{hour} {(hour <= 1.0 ? ResourceText.TITLE_HOUR : ResourceText.TITLE_HOURS)}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
