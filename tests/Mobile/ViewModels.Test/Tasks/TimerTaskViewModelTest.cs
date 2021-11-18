using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ViewModels.Tasks;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Tasks
{
    public class TimerTaskViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetByIdCategoryUseCase>(() => GetByIdCategoryUseCaseBuilder.Instance().Build());

            var viewModel = new TimerTaskViewModel(navigation, useCase);

            viewModel.StartTimerCommand.Should().NotBeNull();
            viewModel.StopTimerCommand.Should().NotBeNull();
            viewModel.AddTaskTitleCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_Command_DeleteCommand()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetByIdCategoryUseCase>(() => GetByIdCategoryUseCaseBuilder.Instance().Build());

            var viewModel = new TimerTaskViewModel(navigation, useCase)
            {
                Title = ""
            };

            Action action = () => viewModel.AddTaskTitleCommand.Execute(null);

            action.Should().NotThrow();
        }
    }
}
