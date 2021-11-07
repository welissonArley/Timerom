﻿using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Views.AboutThisProject;
using Timerom.App.Views.Views.Category;
using Timerom.App.Views.Views.Dashboard;
using Timerom.App.Views.Views.Reports.ActivityAnalytic;
using Timerom.App.Views.Views.Reports.ParetoPrinciple;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

#pragma warning disable S2696
namespace Timerom.App.ViewModels.Dashboard
{
    public class DashboardPageFlyoutViewModel : ViewModelBase
    {
        public IAsyncCommand<MenuItemOptions> MenuItemSelectedCommand { get; private set; }

        private static MenuItemOptions _menu;
        public MenuItemOptions MenuItemSelected
        {
            get => _menu;
        }
        
        public string VersionText { get; set; }

        public DashboardPageFlyoutViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            MenuItemSelectedCommand = new AsyncCommand<MenuItemOptions>(ItemMenuSelected, allowsMultipleExecutions: false);

            VersionText = string.Format(ResourceText.TITLE_VERSION_NUMBER, VersionTracking.CurrentVersion);
        }

        private async Task ItemMenuSelected(MenuItemOptions option)
        {
            _menu = option;

            switch (option)
            {
                case MenuItemOptions.IconsAndIllustrations:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "IlustrationsInformationsPage", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(new Uri($"/DashboardPage/NavigationPage/{nameof(IlustrationsInformationsPage)}", UriKind.Absolute));
                    }
                    break;
                case MenuItemOptions.Dashboard:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "DashboardPageDetail", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(new Uri($"/DashboardPage/NavigationPage/{nameof(DashboardPageDetail)}", UriKind.Absolute));
                    }
                    break;
                case MenuItemOptions.Categories:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "CategoriesPage", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(new Uri($"/DashboardPage/NavigationPage/{nameof(CategoriesPage)}", UriKind.Absolute));
                    }
                    break;
                case MenuItemOptions.ActivityAnalytic:
                    {
                        await _navigationService.NavigateAsync(new Uri($"/DashboardPage/NavigationPage/{nameof(ActivityAnalyticPage)}", UriKind.Absolute));
                    }
                    break;
                case MenuItemOptions.ParetoPrinciple:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "ParetoPrinciplePage", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(new Uri($"/DashboardPage/NavigationPage/{nameof(ChooseDatesParetoPrinciplePage)}", UriKind.Absolute));
                    }
                    break;
                case MenuItemOptions.PrivacyPolicy:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "PrivacyPolicyPage", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(new Uri($"/DashboardPage/NavigationPage/{nameof(PrivacyPolicyPage)}", UriKind.Absolute));
                    }
                    break;
                case MenuItemOptions.UseTerms:
                    {
                        TrackEvent("DashboardMenuFlyoutPage", "TermsOfUsePage", EventFlag.Navigation);
                        await _navigationService.NavigateAsync(new Uri($"/DashboardPage/NavigationPage/{nameof(TermsOfUsePage)}", UriKind.Absolute));
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
