using System;
using System.Globalization;
using Timerom.App.ValueObjects.Enuns;
using Xamarin.Forms;

namespace Timerom.App.Converter
{
    public class CategoryTypeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CategoryType type = (CategoryType)value;
            Color color = GetColor(type);

            return targetType.Equals(typeof(Brush)) ? new SolidColorBrush(color) : (object)color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        private Color GetColor(CategoryType categoryType)
        {
            switch (categoryType)
            {
                case CategoryType.Productive:
                    return Application.Current.RequestedTheme == OSAppTheme.Dark ? (Color)Application.Current.Resources["DarkProductiveColor"] : (Color)Application.Current.Resources["LigthProductiveColor"];
                case CategoryType.Neutral:
                    return Application.Current.RequestedTheme == OSAppTheme.Dark ? (Color)Application.Current.Resources["DarkNeutralColor"] : (Color)Application.Current.Resources["LigthNeutralColor"];
                case CategoryType.Unproductive:
                    return Application.Current.RequestedTheme == OSAppTheme.Dark ? (Color)Application.Current.Resources["DarkUnproductiveColor"] : (Color)Application.Current.Resources["LigthUnproductiveColor"];
                default:
                    return Color.Black;
            }
        }
    }
}
