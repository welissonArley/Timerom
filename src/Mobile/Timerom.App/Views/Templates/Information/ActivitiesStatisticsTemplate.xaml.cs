using System.Globalization;
using Timerom.App.Converter;
using Timerom.App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitiesStatisticsTemplate : ContentView
    {
        public ActivitiesAnalyticModel Activity
        {
            get => (ActivitiesAnalyticModel)GetValue(ActivityProperty);
            set => SetValue(ActivityProperty, value);
        }
        public static readonly BindableProperty ActivityProperty = BindableProperty.Create(
                                                        propertyName: "Activity",
                                                        returnType: typeof(ActivitiesAnalyticModel),
                                                        declaringType: typeof(ActivitiesStatisticsTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: ActivityChanged);

        private static void ActivityChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var activity = (ActivitiesAnalyticModel)(newValue ?? oldValue);

            if (activity == null)
                return;

            var component = (ActivitiesStatisticsTemplate)bindable;

            component.LabelIndex.TextColor = (Color)new CategoryTypeColorConverter().Convert(activity.Type, typeof(Label), null, CultureInfo.CurrentCulture);
            component.LabelIndex.Text = $"{activity.Percentage}%";
            component.Ellipse.Stroke = (SolidColorBrush)new CategoryTypeColorConverter().Convert(activity.Type, typeof(Brush), null, CultureInfo.CurrentCulture);
            component.LabelName.Text = activity.Name;
            component.LabelTime.Text = $"{new HoursStringconverter().Convert(activity.Time, typeof(Label), null, CultureInfo.CurrentCulture)}";
        }

        public ActivitiesStatisticsTemplate()
        {
            InitializeComponent();
        }
    }
}