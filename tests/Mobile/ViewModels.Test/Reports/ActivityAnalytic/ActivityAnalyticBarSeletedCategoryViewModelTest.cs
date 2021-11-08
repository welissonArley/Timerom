using FluentAssertions;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ViewModels.Reports.ActivityAnalytic;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Reports.ActivityAnalytic
{
    public class ActivityAnalyticBarSeletedCategoryViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var useCase = new Lazy<IActivityAnalyticDetailsSubCategoryUseCase>(() => ActivityAnalyticDetailsSubCategoryUseCaseBuilder.Instance().Build());

            var viewModel = new ActivityAnalyticBarSeletedCategoryViewModel(navigation, useCase);

            viewModel.Activities.Should().BeNull();
        }

        [Fact]
        public async Task Validade_Sucess_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var useCase = new Lazy<IActivityAnalyticDetailsSubCategoryUseCase>(() => ActivityAnalyticDetailsSubCategoryUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new ActivityAnalyticBarSeletedCategoryViewModel(navigation, useCase);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Date", DateTime.Today)
                .Parameter("ActivitiesAnalytic", RequestActivitiesAnalyticModel.Instance().Build())
                .Build();

            Func<Task> action = async () => await viewModel.InitializeAsync(parameters);

            await action.Should().NotThrowAsync();

            viewModel.Description.Should().NotBeNullOrWhiteSpace();
            viewModel.Activities.Should().NotBeNullOrEmpty();
        }
    }
}
