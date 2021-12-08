using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;

namespace Timerom.App.Converter
{
    public class FiltersHeightRequestExpanderConverter : IValueConverter
    {
        private const double ITEM_SIZE = 40;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = (ObservableCollection<Model.FilterModel>)value;
            return ITEM_SIZE * list.Count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
