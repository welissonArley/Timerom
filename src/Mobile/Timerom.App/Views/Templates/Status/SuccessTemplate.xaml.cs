using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Status
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessTemplate : ContentView
    {
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }
        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
                                                        propertyName: "Message",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(SuccessTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: MessageChanged);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                                                        propertyName: "Title",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(SuccessTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TitleChanged);

        private static void MessageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (SuccessTemplate)bindable;
            component.LabelMessage.Text = newValue.ToString();
        }
        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (SuccessTemplate)bindable;
            component.LabelTitle.Text = newValue.ToString();
        }

        public SuccessTemplate()
        {
            InitializeComponent();
        }
    }
}