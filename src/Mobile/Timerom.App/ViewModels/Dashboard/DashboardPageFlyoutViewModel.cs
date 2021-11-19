using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Services.Navigation;
using Timerom.App.Services.XamarinEssentials.Interface;
using Timerom.App.ValueObjects.Enuns;
using Xamarin.CommunityToolkit.ObjectModel;

#pragma warning disable S2696
namespace Timerom.App.ViewModels.Dashboard
{
    public class DashboardPageFlyoutViewModel : ViewModelBase
    {
        private readonly Lazy<IMenuPath> menuNavigation;
        private IMenuPath _menuNavigation => menuNavigation.Value;

        public IAsyncCommand<MenuItemOptions> MenuItemSelectedCommand { get; private set; }

        private static MenuItemOptions _menu;
        public MenuItemOptions MenuItemSelected
        {
            get => _menu;
        }
        
        public string VersionText { get; set; }

        public DashboardPageFlyoutViewModel(Lazy<INavigationService> navigationService, Lazy<IMenuPath> menuNavigation, IVersionTracking versionTracking) : base(navigationService)
        {
            this.menuNavigation = menuNavigation;

            MenuItemSelectedCommand = new AsyncCommand<MenuItemOptions>(ItemMenuSelected, allowsMultipleExecutions: false);

            VersionText = string.Format(ResourceText.TITLE_VERSION_NUMBER, versionTracking.CurrentVersion());
        }

        private async Task ItemMenuSelected(MenuItemOptions option)
        {
            _menu = option;

            switch (option)
            {
                case MenuItemOptions.IconsAndIllustrations:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "IlustrationsInformationsPage", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(_menuNavigation.IconsAndIllustrations());
                    }
                    break;
                case MenuItemOptions.Dashboard:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "DashboardPageDetail", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(_menuNavigation.Dashboard());
                    }
                    break;
                case MenuItemOptions.Categories:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "CategoriesPage", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(_menuNavigation.Categories());
                    }
                    break;
                case MenuItemOptions.ActivityAnalytic:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "ActivityAnalytic", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(_menuNavigation.ActivityAnalytic());
                    }
                    break;
                case MenuItemOptions.ParetoPrinciple:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "ParetoPrinciplePage", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(_menuNavigation.ParetoPrinciple());
                    }
                    break;
                case MenuItemOptions.PrivacyPolicy:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "PrivacyPolicyPage", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(_menuNavigation.PrivacyPolicy());
                    }
                    break;
                case MenuItemOptions.UseTerms:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "TermsOfUsePage", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(_menuNavigation.UseTerms());
                    }
                    break;
                case MenuItemOptions.ContactUs:
                    break;
                default:
                    break;
            }
        }

        public static void Initialize()
        {
            _menu = MenuItemOptions.Dashboard;
        }
    }
}
