using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.ViewModels.Home;
using Useful.ToTests.Builders.Navigation;
using Xunit;

namespace ViewModels.Test.Home
{
    public class HomePageViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new HomePageViewModel(navigation);

            viewModel.HomeCommand.Should().NotBeNull();
            viewModel.AddTaskCommand.Should().NotBeNull();
            viewModel.StartTaskCommand.Should().NotBeNull();
            viewModel.ShowReportCommand.Should().NotBeNull();
            viewModel.ThereIsTimer.Should().BeFalse();
        }

        [Fact]
        public void Validade_OnNavigatedFrom_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new HomePageViewModel(navigation);

            Action action = () => viewModel.OnNavigatedFrom(null);

            action.Should().NotThrow();
        }
    }
}
