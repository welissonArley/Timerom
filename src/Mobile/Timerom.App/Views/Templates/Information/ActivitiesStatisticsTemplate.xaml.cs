using System;
using System.Globalization;
using Timerom.App.Converter;
using Timerom.App.Model;
using Xamarin.CommunityToolkit.ObjectModel;
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

        public IAsyncCommand<long> ItemSelectedCommand
        {
            get => (IAsyncCommand<long>)GetValue(ItemSelectedCommandProperty);
            set => SetValue(ItemSelectedCommandProperty, value);
        }
        public static readonly BindableProperty ItemSelectedCommandProperty = BindableProperty.Create(
                                                        propertyName: "ItemSelectedCommand",
                                                        returnType: typeof(IAsyncCommand<long>),
                                                        declaringType: typeof(ActivitiesStatisticsTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: ItemSelectedCommandChanged);

        private static void ItemSelectedCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var command = (IAsyncCommand<long>)(newValue ?? oldValue);

            if (command == null)
                return;

            var component = (ActivitiesStatisticsTemplate)bindable;
            component.IconArrow.IsVisible = true;
        }

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

        private void Item_Tapped(object sender, EventArgs e)
        {
            ItemSelectedCommand?.Execute(Activity.Id);
        }
    }
}