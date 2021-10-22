using System;
using System.Globalization;
using Timerom.App.ValueObjects.Enuns;
using Xamarin.Forms;

namespace Timerom.App.Converter
{
    public class CategoryTypeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (CategoryType)value;
            return GetString(type);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        private string GetString(CategoryType categoryType)
        {
            switch (categoryType)
            {
                case CategoryType.Productive:
                    return ResourceText.TITLE_PRODUCTIVE;
                case CategoryType.Neutral:
                    return ResourceText.TITLE_NEUTRAL;
                case CategoryType.Unproductive:
                    return ResourceText.TITLE_UNPRODUCTIVE;
                default:
                    return "";
            }
        }
    }
}
