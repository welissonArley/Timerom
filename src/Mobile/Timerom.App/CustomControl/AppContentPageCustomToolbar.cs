using System.ComponentModel;
using Xamarin.Forms;

namespace Timerom.App.CustomControl
{
    public class AppContentPageCustomToolbar : ContentPage
    {
        public AppToolbarItem CustomToolbar
        {
            get { return (AppToolbarItem)GetValue(CustomToolbarProperty); }
            set { SetValue(CustomToolbarProperty, value); }
        }
        public static readonly BindableProperty CustomToolbarProperty = BindableProperty.Create(
            propertyName: "CustomToolbar", returnType: typeof(AppToolbarItem), declaringType: typeof(AppContentPageCustomToolbar),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay, propertyChanged: CustomToolbarChanged);

        private static void CustomToolbarChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (AppToolbarItem)newValue;
            var component = (AppContentPageCustomToolbar)bindable;

            item.PropertyChanged += (sender, e) =>
            {
                propertyChanged(e, component, item);
            };
        }

        private static void propertyChanged(PropertyChangedEventArgs e, AppContentPageCustomToolbar appContent, AppToolbarItem item)
        {
            if(e.PropertyName.Equals("IsVisible"))
                UpdateToolbar(appContent, item);
        }

        private static void UpdateToolbar(AppContentPageCustomToolbar component, AppToolbarItem item)
        {
            if (item.IsVisible)
                component.ToolbarItems.Add(item);
            else
                component.ToolbarItems.Clear();
        }
    }
}
