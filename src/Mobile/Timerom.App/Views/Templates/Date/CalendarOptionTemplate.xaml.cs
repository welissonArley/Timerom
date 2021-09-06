using System;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Date
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarOptionTemplate : ContentView
    {
        public IAsyncCommand<DateTime> OnDateChanged
        {
            get => (IAsyncCommand<DateTime>)GetValue(OnDateChangedProperty);
            set => SetValue(OnDateChangedProperty, value);
        }
        public static readonly BindableProperty OnDateChangedProperty = BindableProperty.Create(
                                                        propertyName: "OnDateChanged",
                                                        returnType: typeof(IAsyncCommand<DateTime>),
                                                        declaringType: typeof(CalendarOptionTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

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
            component.LabelDate.Date = (DateTime)newValue;
        }

        public CalendarOptionTemplate()
        {
            InitializeComponent();
        }

        private void LabelDate_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Date"))
                OnDateChanged?.Execute(((DatePicker)sender).Date);
        }
    }
}