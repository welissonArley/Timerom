using System;
using Timerom.App.Services.AppVersion;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageFlyout : ContentPage
    {
        private Action<MenuItemOptions> _callBackItemMenuSelectedAction;

        public HomePageFlyout()
        {
            InitializeComponent();

            IAppVersion appVersion = DependencyService.Get<IAppVersion>();

            LabelVersion.Text = string.Format(ResourceText.TITLE_VERSION_NUMBER, appVersion.GetVersionNumber());

            ChangeBindingContext(MenuItemOptions.Dashboard);
        }

        private void IconsAndIllustrationsUsed_Tapped(object sender, EventArgs e)
        {
            ChangeBindingContext(MenuItemOptions.IconsAndIllustrations);
            _callBackItemMenuSelectedAction(MenuItemOptions.IconsAndIllustrations);
        }
        private void Dashboard_Tapped(object sender, EventArgs e)
        {
            ChangeBindingContext(MenuItemOptions.Dashboard);
            _callBackItemMenuSelectedAction(MenuItemOptions.Dashboard);
        }

        private void ChangeBindingContext(MenuItemOptions selected)
        {
            BindingContext = new HomePageFlyoutViewModel(selected);
        }
        public void SetCallbackMenuSelected(Action<MenuItemOptions> action)
        {
            _callBackItemMenuSelectedAction = action;
        }
    }
}