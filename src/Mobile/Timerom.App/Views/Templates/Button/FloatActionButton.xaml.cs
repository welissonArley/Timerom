using System;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Button
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatActionButton : ContentView
    {
        public IAsyncCommand TappedButtonCommand
        {
            get => (IAsyncCommand)GetValue(TappedButtonCommandProperty);
            set => SetValue(TappedButtonCommandProperty, value);
        }
        public static readonly BindableProperty TappedButtonCommandProperty = BindableProperty.Create(propertyName: "TappedButton",
                                                        returnType: typeof(IAsyncCommand),
                                                        declaringType: typeof(FloatActionButton),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public FloatActionButton()
        {
            InitializeComponent();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            TappedButtonCommand?.Execute(null);
        }
    }
}