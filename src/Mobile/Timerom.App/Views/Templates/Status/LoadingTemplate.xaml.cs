using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Status
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingTemplate : ContentView
    {
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }
        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
                                                        propertyName: "Message",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(LoadingTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: MessageChanged);

        private static void MessageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (LoadingTemplate)bindable;
            component.LabelText.Text = newValue.ToString();
        }

        public LoadingTemplate()
        {
            InitializeComponent();
        }
    }
}