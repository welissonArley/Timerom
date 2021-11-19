using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.Services.Navigation;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ViewModels.Dashboard;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Service;
using Xunit;

namespace ViewModels.Test.Dashboard
{
    public class DashboardPageFlyoutViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var service = VersionTrackingBuilder.Instance().Build();
            var menuPath = new Lazy<IMenuPath>(() => MenuPathBuilder.Instance().Build());

            var viewModel = new DashboardPageFlyoutViewModel(navigation, menuPath, service);
            DashboardPageFlyoutViewModel.Initialize();

            viewModel.MenuItemSelectedCommand.Should().NotBeNull();
            viewModel.VersionText.Should().NotBeNull();
            viewModel.MenuItemSelected.Should().Equals(MenuItemOptions.Dashboard);
        }

        [Theory]
        [InlineData(MenuItemOptions.IconsAndIllustrations)]
        [InlineData(MenuItemOptions.Dashboard)]
        [InlineData(MenuItemOptions.Categories)]
        [InlineData(MenuItemOptions.ActivityAnalytic)]
        [InlineData(MenuItemOptions.ParetoPrinciple)]
        [InlineData(MenuItemOptions.PrivacyPolicy)]
        [InlineData(MenuItemOptions.UseTerms)]
        [InlineData(MenuItemOptions.ContactUs)]
        [InlineData((MenuItemOptions)1000)]
        public void Validade_Command_MenuItemSelected(MenuItemOptions menuItem)
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var service = VersionTrackingBuilder.Instance().Build();
            var menuPath = new Lazy<IMenuPath>(() => MenuPathBuilder.Instance().Build());

            var viewModel = new DashboardPageFlyoutViewModel(navigation, menuPath, service);

            Action action = () => viewModel.MenuItemSelectedCommand.Execute(menuItem);

            action.Should().NotThrow();
        }
    }
}
