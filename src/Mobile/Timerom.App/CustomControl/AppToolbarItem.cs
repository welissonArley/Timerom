using Xamarin.Forms;

namespace Timerom.App.CustomControl
{
    public class AppToolbarItem : ToolbarItem
    {
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
            propertyName: "IsVisible", returnType: typeof(bool), declaringType: typeof(AppToolbarItem), defaultValue: false,
            defaultBindingMode: BindingMode.OneWay, propertyChanged: null);
    }
}
