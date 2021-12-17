using Timerom.App.ValueObjects.Dto;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepProgressTemplate : ContentView
    {
        public StepConfig StepConfig
        {
            get => (StepConfig)GetValue(StepConfigProperty);
            set => SetValue(StepConfigProperty, value);
        }
        public static readonly BindableProperty StepConfigProperty = BindableProperty.Create(
                                                        propertyName: "StepConfig",
                                                        returnType: typeof(StepConfig),
                                                        declaringType: typeof(StepProgressTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: StepConfigChanged);

        private static void StepConfigChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var configObject = oldValue != null && newValue == null ? oldValue : newValue;

            var component = (StepProgressTemplate)bindable;
            var stepConfig = (StepConfig)configObject;

            for (var index = 1; index <= stepConfig.TotalNumberOfSteps; index++)
            {
                if(index <= stepConfig.StepsCompleted)
                    component.StepContent.Children.Add(CompleteStep(index));
                else
                    component.StepContent.Children.Add(NextStep(index));

                if (index < stepConfig.TotalNumberOfSteps)
                    component.StepContent.Children.Add(LineStep());
            }
        }

        private static AvatarView CompleteStep(int index)
        {
            return new AvatarView
            {
                TextColor = Application.Current.RequestedTheme == OSAppTheme.Light ? Color.White : Color.Black,
                Color = Application.Current.RequestedTheme == OSAppTheme.Light ? Color.Black : Color.White,
                Size = 16,
                Text = $"{index}"
            };
        }

        private static AvatarView NextStep(int index)
        {
            return new AvatarView
            {
                TextColor = Application.Current.RequestedTheme == OSAppTheme.Light ? Color.Black : Color.White,
                Color = Application.Current.RequestedTheme == OSAppTheme.Light ? Color.White : Color.Black,
                BorderColor = Application.Current.RequestedTheme == OSAppTheme.Light ? Color.Black : Color.White,
                Size = 16,
                Text = $"{index}"
            };
        }

        private static BoxView LineStep()
        {
            return new BoxView
            {
                Color = Application.Current.RequestedTheme == OSAppTheme.Light ? Color.Black : Color.White,
                HeightRequest = 2,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 30
            };
        }

        public StepProgressTemplate()
        {
            InitializeComponent();
        }
    }
}