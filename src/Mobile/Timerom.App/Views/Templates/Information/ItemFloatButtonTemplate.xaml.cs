using FFImageLoading.Transformations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemFloatButtonTemplate : ContentView
    {
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
                                                        propertyName: "TextColor",
                                                        returnType: typeof(Color),
                                                        declaringType: typeof(CategoryExpanderComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TextColorChanged);
        public string ItemName
        {
            get => (string)GetValue(ItemNameProperty);
            set => SetValue(ItemNameProperty, value);
        }
        public static readonly BindableProperty ItemNameProperty = BindableProperty.Create(
                                                        propertyName: "ItemName",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(CategoryExpanderComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: ItemNameChanged);

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public static readonly BindableProperty IconProperty = BindableProperty.Create(
                                                        propertyName: "Icon",
                                                        returnType: typeof(ImageSource),
                                                        declaringType: typeof(CategoryExpanderComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: IconChanged);

        private static void TextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var color = (Color)newValue;

            var component = (ItemFloatButtonTemplate)bindable;
            component.OptionName.TextColor = color;
            component.SvgImage.Transformations.Add(new TintTransformation { HexColor = color.ToHex(), EnableSolidColor = true});
        }
        private static void ItemNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (ItemFloatButtonTemplate)bindable;
            component.OptionName.Text = (string)newValue;
        }
        private static void IconChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (ItemFloatButtonTemplate)bindable;
            component.SvgImage.Source = (ImageSource)newValue;
        }

        public ItemFloatButtonTemplate()
        {
            InitializeComponent();
        }
    }
}