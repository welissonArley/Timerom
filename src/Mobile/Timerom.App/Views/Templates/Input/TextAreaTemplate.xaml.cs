using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Input
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextAreaTemplate : ContentView
    {
        public string PlaceHolderText { set => Input.Placeholder = value; get => Input.Placeholder; }

        public object BindingEntry
        {
            get => GetValue(BindindEntryProperty);
            set => SetValue(BindindEntryProperty, value);
        }
        public static readonly BindableProperty BindindEntryProperty = BindableProperty.Create(
                                                        propertyName: "BindingEntry",
                                                        returnType: typeof(object),
                                                        declaringType: typeof(TextAreaTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: BindindEntryChanged);

        private static void BindindEntryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var bindableComponent = (TextAreaTemplate)bindable;
            bindableComponent.Input.SetBinding(Entry.TextProperty, (Binding)newValue);
        }

        public TextAreaTemplate()
        {
            InitializeComponent();
        }
    }
}