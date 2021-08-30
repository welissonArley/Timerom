using Timerom.App.Converter;
using Timerom.App.ValueObjects.Enuns;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressLineTemplate : ContentView
    {
        public CategoryType Category
        {
            get => (CategoryType)GetValue(CategoryTypeProperty);
            set => SetValue(CategoryTypeProperty, value);
        }
        public static readonly BindableProperty CategoryTypeProperty = BindableProperty.Create(
                                                        propertyName: "Category",
                                                        returnType: typeof(CategoryType),
                                                        declaringType: typeof(ProgressLineTemplate),
                                                        defaultValue: (CategoryType)1000,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: CategoryTypeChanged);

        public int Percentage
        {
            get => (int)GetValue(PercentageProperty);
            set => SetValue(PercentageProperty, value);
        }
        public static readonly BindableProperty PercentageProperty = BindableProperty.Create(
                                                        propertyName: "Percentage",
                                                        returnType: typeof(int),
                                                        declaringType: typeof(ProgressLineTemplate),
                                                        defaultValue: -1,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: PercentageChanged);

        private static void CategoryTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var category = (CategoryType)newValue;

            var component = (ProgressLineTemplate)bindable;
            component.BackgroundLine.Color = component.ForegroundLine.Color =
                component.LabelPercentage.TextColor = GetColor(category);

            component.LabelCategory.Text = new CategoryTypeStringConverter().Convert(category, null, null, null).ToString();
        }

        private static void PercentageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (ProgressLineTemplate)bindable;

            var value = (int)newValue;

            var width = Application.Current.MainPage.Width;

            component.LabelPercentage.Text = $"{(value < 10 ? "0" : "")}{value}%";
            component.ForegroundLine.WidthRequest = value == 0 ? 0.7 : (width - 90) * value / 100;
        }

        private static Color GetColor(CategoryType category)
        {
            switch (category)
            {
                case CategoryType.Productive:
                    {
                        return Application.Current.RequestedTheme == OSAppTheme.Light ?
                            (Color)Application.Current.Resources["LigthProductiveColor"] : (Color)Application.Current.Resources["DarkProductiveColor"];
                    }
                case CategoryType.Neutral:
                    {
                        return Application.Current.RequestedTheme == OSAppTheme.Light ?
                            (Color)Application.Current.Resources["LigthNeutralColor"] : (Color)Application.Current.Resources["DarkNeutralColor"];
                    }
                case CategoryType.Unproductive:
                    {
                        return Application.Current.RequestedTheme == OSAppTheme.Light ?
                            (Color)Application.Current.Resources["LigthUnproductiveColor"] : (Color)Application.Current.Resources["DarkUnproductiveColor"];
                    }
                default:
                    return Xamarin.Forms.Color.Black;
            }
        }

        public ProgressLineTemplate()
        {
            InitializeComponent();
        }
    }
}