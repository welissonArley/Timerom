using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Date
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarOptionTemplate : ContentView
    {
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }
        public static readonly BindableProperty DateProperty = BindableProperty.Create(
                                                        propertyName: "Date",
                                                        returnType: typeof(DateTime),
                                                        declaringType: typeof(CalendarOptionTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: DateChanged);

        private static void DateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (CalendarOptionTemplate)bindable;
            component.LabelDate.Text = ((DateTime)newValue).ToString("dd MMMM, yyyy", DateTimeFormatInfo.InvariantInfo);
        }

        public CalendarOptionTemplate()
        {
            InitializeComponent();
        }
    }
}