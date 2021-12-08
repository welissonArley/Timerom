using FluentAssertions;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Dto;
using Timerom.App.ViewModels.Reports.ParetoPrinciple;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Reports.ParetoPrinciple
{
    public class ParetoPrincipleResultViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var useCase = new Lazy<IGetParetoPrincipleResultUseCase>(() => GetParetoPrincipleResultUseCaseBuilder.Instance().Build());

            var viewModel = new ParetoPrincipleResultViewModel(navigation, useCase);

            viewModel.ItemSelectedCommand.Should().NotBeNull();
            viewModel.Model.Should().BeNull();
            viewModel.Period.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Validade_Sucess_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var useCase = new Lazy<IGetParetoPrincipleResultUseCase>(() => GetParetoPrincipleResultUseCaseBuilder.Instance()
                .Execute().Build());

            var viewModel = new ParetoPrincipleResultViewModel(navigation, useCase);

            var startsAt = DateTime.Today;
            var endsAt = DateTime.Today;

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Period", $"{startsAt.ToShortDateString()} - {endsAt.ToShortDateString()}")
                .Parameter("Filter", new ParetoPrincipleFilter { StartsAt = startsAt, EndsAt = endsAt })
                .Build();

            Func<Task> action = async () => await viewModel.InitializeAsync(parameters);

            await action.Should().NotThrowAsync();

            viewModel.Model.Should().NotBeNull();
            viewModel.Period.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Validade_Command_ItemSelected()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var useCase = new Lazy<IGetParetoPrincipleResultUseCase>(() => GetParetoPrincipleResultUseCaseBuilder.Instance().Build());

            var viewModel = new ParetoPrincipleResultViewModel(navigation, useCase);

            var startsAt = DateTime.Today;
            var endsAt = DateTime.Today;

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Period", $"{startsAt.ToShortDateString()} - {endsAt.ToShortDateString()}")
                .Parameter("Filter", new ParetoPrincipleFilter { StartsAt = startsAt, EndsAt = endsAt })
                .Build();

            await viewModel.InitializeAsync(parameters);

            Action action = () => viewModel.ItemSelectedCommand.Execute(1L);

            action.Should().NotThrow();
        }
    }
}
