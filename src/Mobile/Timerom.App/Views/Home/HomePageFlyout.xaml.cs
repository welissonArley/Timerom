using Timerom.App.Services.AppVersion;
using Timerom.App.ValueObjects.Enuns;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageFlyout : ContentPage
    {
        public HomePageFlyout()
        {
            InitializeComponent();

            IAppVersion appVersion = DependencyService.Get<IAppVersion>();

            LabelVersion.Text = string.Format(ResourceText.TITLE_VERSION_NUMBER, appVersion.GetVersionNumber());

            BindingContext = new HomePageFlyoutViewModel(MenuItemOptions.Dashboard);
        }
    }

    public class HomePageFlyoutViewModel
    {
        public MenuItemOptions? MenuItemSelected { get; set; } = null;

        public HomePageFlyoutViewModel(MenuItemOptions selected)
        {
            MenuItemSelected = selected;
        }
    }
}