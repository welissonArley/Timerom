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

        public object BindingEntry
        {
            get => GetValue(BindindEntryProperty);
            set => SetValue(BindindEntryProperty, value);
        }
        public static readonly BindableProperty BindindEntryProperty = BindableProperty.Create(
                                                        propertyName: "BindingEntry",
                                                        returnType: typeof(object),
                                                        declaringType: typeof(InputTextWithLabelComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: BindindEntryChanged);

        private static void BindindEntryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var bindableComponent = (InputTextWithLabelComponent)bindable;
            bindableComponent.Entry.SetBinding(Xamarin.Forms.Entry.TextProperty, (Binding)newValue);
        }
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