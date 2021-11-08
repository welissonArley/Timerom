using FluentAssertions;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ViewModels.Reports.ActivityAnalytic;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Reports.ActivityAnalytic
{
    public class ActivityAnalyticViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var totalUseCase = new Lazy<IGetActivityAnalyticTotalUseCase>(() => GetActivityAnalyticTotalUseCaseBuilder.Instance().Build());
            var chartUseCase = new Lazy<IGetChartActivityAnalyticUseCase>(() => GetChartActivityAnalyticUseCaseBuilder.Instance().Build());

            var viewModel = new ActivityAnalyticViewModel(navigation, totalUseCase, chartUseCase);

            viewModel.DaySelectedCommand.Should().NotBeNull();
            viewModel.AnalyticModel.Should().BeNull();
        }

        [Fact]
        public void Validade_Command_DaySelected()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var totalUseCase = new Lazy<IGetActivityAnalyticTotalUseCase>(() => GetActivityAnalyticTotalUseCaseBuilder.Instance().Build());
            var chartUseCase = new Lazy<IGetChartActivityAnalyticUseCase>(() => GetChartActivityAnalyticUseCaseBuilder.Instance().Build());

            var viewModel = new ActivityAnalyticViewModel(navigation, totalUseCase, chartUseCase);

            Action action = () => viewModel.DaySelectedCommand.Execute(DateTime.Now);

            action.Should().NotThrow();
        }

        [Fact]
        public async Task Validade_Sucess_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var totalUseCase = new Lazy<IGetActivityAnalyticTotalUseCase>(() => GetActivityAnalyticTotalUseCaseBuilder.Instance().Execute().Build());
            var chartUseCase = new Lazy<IGetChartActivityAnalyticUseCase>(() => GetChartActivityAnalyticUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new ActivityAnalyticViewModel(navigation, totalUseCase, chartUseCase);

            Func<Task> action = async () => await viewModel.InitializeAsync(null);

            await action.Should().NotThrowAsync();

            viewModel.ChartModel.Should().NotBeNullOrEmpty();
            viewModel.AnalyticModel.Should().NotBeNull();
            viewModel.CurrentState.Should().Equals(Xamarin.CommunityToolkit.UI.Views.LayoutState.None);
        }

        [Fact]
        public async Task Validade_Sucess_Initialize_Empty()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var totalUseCase = new Lazy<IGetActivityAnalyticTotalUseCase>(() => GetActivityAnalyticTotalUseCaseBuilder.Instance().Build());
            var chartUseCase = new Lazy<IGetChartActivityAnalyticUseCase>(() => GetChartActivityAnalyticUseCaseBuilder.Instance().Empty().Build());

            var viewModel = new ActivityAnalyticViewModel(navigation, totalUseCase, chartUseCase);

            Func<Task> action = async () => await viewModel.InitializeAsync(null);

            await action.Should().NotThrowAsync();

            viewModel.ChartModel.Should().BeEmpty();
            viewModel.CurrentState.Should().Equals(Xamarin.CommunityToolkit.UI.Views.LayoutState.Empty);
        }
    }
}
