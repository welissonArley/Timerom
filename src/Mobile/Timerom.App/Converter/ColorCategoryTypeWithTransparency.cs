using System;
using System.Globalization;
using Xamarin.Forms;

namespace Timerom.App.Converter
{
    public class ColorCategoryTypeWithTransparency : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color) new CategoryTypeColorConverter().Convert(value, targetType, parameter, culture);

            return color.MultiplyAlpha(0.1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
