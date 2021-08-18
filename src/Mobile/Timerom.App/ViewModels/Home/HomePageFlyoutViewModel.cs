using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;
using Timerom.App.Services.AppVersion;
using Timerom.App.ValueObjects.Enuns;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Home
{
    public class HomePageFlyoutViewModel : BindableBase
    {
        public IAsyncCommand<MenuItemOptions> MenuItemSelectedCommand { get; private set; }

        private readonly INavigationService _navigationService;

        private static MenuItemOptions _menu;
        public MenuItemOptions MenuItemSelected
        {
            get => _menu;
        }
        public string VersionText { get; set; }

        public HomePageFlyoutViewModel(INavigationService navigationService, IAppVersion appVersion)
        {
            MenuItemSelectedCommand = new AsyncCommand<MenuItemOptions>(ItemMenuSelected, allowsMultipleExecutions: false);

            _navigationService = navigationService;

            VersionText = string.Format(ResourceText.TITLE_VERSION_NUMBER, appVersion.GetVersionNumber());
        }

        private async Task ItemMenuSelected(MenuItemOptions option)
        {
            _menu = option;

            switch (option)
            {
                case MenuItemOptions.IconsAndIllustrations:
                    {
                        await _navigationService.NavigateAsync(new System.Uri("/HomePage/NavigationPage/IlustrationsInformationsPage", System.UriKind.Absolute));
                    }
                    break;
                case MenuItemOptions.Dashboard:
                    {
                        await _navigationService.NavigateAsync(new System.Uri("/HomePage/NavigationPage/HomePageDetail", System.UriKind.Absolute));
                    }
                    break;
                case MenuItemOptions.Categories:
                    {
                        await _navigationService.NavigateAsync(new System.Uri("/HomePage/NavigationPage/CategoriesPage", System.UriKind.Absolute));
                    }
                    break;
                case MenuItemOptions.PrivacyPolicy:
                    break;
                case MenuItemOptions.UseTerms:
                    break;
                case MenuItemOptions.ContactUs:
                    break;
                default:
                    break;
            }
        }
    }
}
