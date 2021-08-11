﻿using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.AboutThisProject;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : FlyoutPage
    {
        HomePageFlyout flyoutPage;

        public HomePage()
        {
            InitializeComponent();

            flyoutPage = new HomePageFlyout();
            Flyout = flyoutPage;

            flyoutPage.SetCallbackMenuSelected(ItemMenuSelected);
        }

        private void ItemMenuSelected(MenuItemOptions option)
        {
            IsPresented = false;

            switch (option)
            {
                case MenuItemOptions.IconsAndIllustrations:
                    {
                        Detail = new NavigationPage(new IlustrationsInformationsPage());
                    }
                    break;
                case MenuItemOptions.Dashboard:
                    {
                        Detail = new NavigationPage(new HomePageDetail());
                    }
                    break;
                case MenuItemOptions.Categories:
                    break;
                case MenuItemOptions.PrivacyPolicy:
                    break;
                case MenuItemOptions.UseTerms:
                    break;
                case MenuItemOptions.ContactUs:
                    break;
            }
        }
    }
}