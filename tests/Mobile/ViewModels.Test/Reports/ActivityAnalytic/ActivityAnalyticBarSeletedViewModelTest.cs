using FluentAssertions;
using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ViewModels.Reports.ActivityAnalytic;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Reports.ActivityAnalytic
{
    public class ActivityAnalyticBarSeletedViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var useCase = new Lazy<IActivityAnalyticDetailsUseCase>(() => ActivityAnalyticDetailsUseCaseBuilder.Instance().Build());

            var viewModel = new ActivityAnalyticBarSeletedViewModel(navigation, useCase);

            viewModel.ActivitySelectedCommand.Should().NotBeNull();
            viewModel.AnalyticModel.Should().BeNull();
            viewModel.Description.Should().BeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Validade_Sucess_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var useCase = new Lazy<IActivityAnalyticDetailsUseCase>(() => ActivityAnalyticDetailsUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new ActivityAnalyticBarSeletedViewModel(navigation, useCase);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Date", DateTime.Today)
                .Build();

            Func<Task> action = async () => await viewModel.InitializeAsync(parameters);

            await action.Should().NotThrowAsync();

            viewModel.Description.Should().NotBeNullOrWhiteSpace();
            viewModel.AnalyticModel.Should().NotBeNull();
        }

        [Fact]
        public async Task Validade_Command_ActivitySelected()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var useCase = new Lazy<IActivityAnalyticDetailsUseCase>(() => ActivityAnalyticDetailsUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new ActivityAnalyticBarSeletedViewModel(navigation, useCase);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Date", DateTime.Today)
                .Build();

            await viewModel.InitializeAsync(parameters);

            Action action = () => viewModel.ActivitySelectedCommand.Execute(viewModel.AnalyticModel.Activities.First().Id);

            action.Should().NotThrow();
        }
    }
}
