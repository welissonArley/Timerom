using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Timerom.App.Converter;
using Timerom.App.Model;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartActivityAnalyticTemplate : ContentView
    {
        private static IAsyncCommand<DateTime> _daySelectedCommand;

        public IAsyncCommand<DateTime> DaySelectedCommand
        {
            get => (IAsyncCommand<DateTime>)GetValue(DaySelectedCommandProperty);
            set => SetValue(DaySelectedCommandProperty, value);
        }
        public static readonly BindableProperty DaySelectedCommandProperty = BindableProperty.Create(
                                                        propertyName: "DaySelectedCommand",
                                                        returnType: typeof(IAsyncCommand<DateTime>),
                                                        declaringType: typeof(ChartActivityAnalyticTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: DaySelectedCommandChanged);

        private static void DaySelectedCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            _daySelectedCommand = (IAsyncCommand<DateTime>)(newValue ?? oldValue);
        }

        public ObservableCollection<ChartActivityAnalyticModel> Values
        {
            get => (ObservableCollection<ChartActivityAnalyticModel>)GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }

        public static readonly BindableProperty ValuesProperty = BindableProperty.Create(
                                                        propertyName: "Values",
                                                        returnType: typeof(ObservableCollection<ChartActivityAnalyticModel>),
                                                        declaringType: typeof(ChartActivityAnalyticTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: ValuesChanged);

        private static void ValuesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var chartActivities = (ObservableCollection<ChartActivityAnalyticModel>)(newValue ?? oldValue);

            if (chartActivities == null)
                return;

            var component = (ChartActivityAnalyticTemplate)bindable;

            var today = DateTime.Today;
            int index = 0;
            var maxValue = chartActivities.Any() ? chartActivities.Max(c => c.Time.TotalMinutes) : 0;

            component.GridContent.Children.Clear();

            foreach (var activity in chartActivities)
            {
                var stackLayout = new StackLayout
                {
                    Padding = 0,
                    Spacing = 0,
                    Children =
                    {
                        new Line
                        {
                            BackgroundColor = (Color)new CategoryTypeColorConverter().Convert(activity.Type, typeof(Line), null, CultureInfo.CurrentCulture),
                            Opacity = 1,
                            HeightRequest = activity.Time.TotalMinutes * 111 / maxValue,
                            VerticalOptions = LayoutOptions.EndAndExpand
                        }
                    }
                };
                stackLayout.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new AsyncCommand(async() => { await _daySelectedCommand?.ExecuteAsync(activity.Date); }, allowsMultipleExecutions: false)
                });

                component.GridContent.Children.Add(stackLayout, index, 0);
                component.GridContent.Children.Add(new Label
                {
                    FontSize = 11,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = activity.Date.ToString("ddd"),
                    Style = activity.Date.Day == today.Day ? (Style)Application.Current.Resources["LabelSemiBold"] : (Style)Application.Current.Resources["LabelBaseStyle"]
                }, index, 1);
                component.GridContent.Children.Add(new Label
                {
                    FontSize = 11,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = activity.Date.ToString("dd"),
                    Style = activity.Date.Day == today.Day ? (Style)Application.Current.Resources["LabelSemiBold"] : (Style)Application.Current.Resources["LabelBaseStyle"]
                }, index, 2);

                index++;
            }

            for(var secondIndex = index; secondIndex < 7; secondIndex++)
            {
                component.GridContent.Children.Add(new Line
                {
                    BackgroundColor = Color.Black,
                    Opacity = 1,
                    HeightRequest = .2,
                    VerticalOptions = LayoutOptions.End
                }, secondIndex, 0);
                component.GridContent.Children.Add(new Label
                {
                    FontSize = 11,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "-"
                }, secondIndex, 1);
                component.GridContent.Children.Add(new Label
                {
                    FontSize = 11,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "-"
                }, secondIndex, 2);
            }
        }

        public ChartActivityAnalyticTemplate()
        {
            InitializeComponent();
        }
    }
}