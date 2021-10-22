using System;
using System.Globalization;
using Timerom.App.ValueObjects.Formater;
using Xamarin.Forms;

namespace Timerom.App.Converter
{
    public class TimeSpanStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan time = (TimeSpan)value;

            return new TimeSpanToCompleteStringFormater().Format(time);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
