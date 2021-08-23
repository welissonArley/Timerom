using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Services.AppVersion;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Views.AboutThisProject;
using Timerom.App.Views.Views.Category;
using Timerom.App.Views.Views.Home;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Home
{
    public class HomePageFlyoutViewModel : BindableBase
    {
        private readonly Lazy<INavigationService> navigationService;
        private INavigationService _navigationService => navigationService.Value;

        public IAsyncCommand<MenuItemOptions> MenuItemSelectedCommand { get; private set; }

        private static MenuItemOptions _menu;
        public MenuItemOptions MenuItemSelected
        {
            get => _menu;
        }
        
        public string VersionText { get; set; }

        public HomePageFlyoutViewModel(Lazy<INavigationService> navigationService, IAppVersion appVersion)
        {
            MenuItemSelectedCommand = new AsyncCommand<MenuItemOptions>(ItemMenuSelected, allowsMultipleExecutions: false);

            this.navigationService = navigationService;

            VersionText = string.Format(ResourceText.TITLE_VERSION_NUMBER, appVersion.GetVersionNumber());
        }

        private async Task ItemMenuSelected(MenuItemOptions option)
        {
            _menu = option;

            switch (option)
            {
                case MenuItemOptions.IconsAndIllustrations:
                    {
                        await _navigationService.NavigateAsync(new Uri($"/HomePage/NavigationPage/{nameof(IlustrationsInformationsPage)}", UriKind.Absolute));
                    }
                    break;
                case MenuItemOptions.Dashboard:
                    {
                        await _navigationService.NavigateAsync(new Uri($"/HomePage/NavigationPage/{nameof(HomePageDetail)}", UriKind.Absolute));
                    }
                    break;
                case MenuItemOptions.Categories:
                    {
                        await _navigationService.NavigateAsync(new Uri($"/HomePage/NavigationPage/{nameof(CategoriesPage)}", UriKind.Absolute));
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
