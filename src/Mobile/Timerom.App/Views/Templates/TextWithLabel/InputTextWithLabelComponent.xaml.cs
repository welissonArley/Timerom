using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.TextWithLabel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputTextWithLabelComponent : ContentView
    {
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                                                        propertyName: "Title",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(InputTextWithLabelComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TitleChanged);

        public string PlaceHolder
        {
            get => (string)GetValue(PlaceHolderProperty);
            set => SetValue(PlaceHolderProperty, value);
        }
        public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(
                                                        propertyName: "PlaceHolder",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(InputTextWithLabelComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: PlaceHolderChanged);

        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (InputTextWithLabelComponent)bindable;
            component.LabelTitle.Text = newValue.ToString();
        }
        private static void PlaceHolderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (InputTextWithLabelComponent)bindable;
            component.Entry.Placeholder = newValue.ToString();
        }

        public InputTextWithLabelComponent()
        {
            InitializeComponent();
        }
    }
}