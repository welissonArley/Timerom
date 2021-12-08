using FluentAssertions;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Services.BackGroundService;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ViewModels.Tasks;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.Service;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Tasks
{
    public class TimerTaskViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().Build());
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetByIdCategoryUseCase>(() => GetByIdCategoryUseCaseBuilder.Instance().Build());

            var viewModel = new TimerTaskViewModel(navigation, useCase, timerUserTask);

            viewModel.StartTimerCommand.Should().NotBeNull();
            viewModel.StopTimerCommand.Should().NotBeNull();
            viewModel.AddTaskTitleCommand.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(TimerTaskInlineDataTest))]
        public void Validade_Command_AddTaskTitle(string title)
        {
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().Build());
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetByIdCategoryUseCase>(() => GetByIdCategoryUseCaseBuilder.Instance().Build());

            var viewModel = new TimerTaskViewModel(navigation, useCase, timerUserTask)
            {
                Title = title
            };

            Action action = () => viewModel.AddTaskTitleCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(TimerTaskInlineDataTest))]
        public void Validate_Callback_AddTaskTitle(string title)
        {
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().Build());
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().ExecuteCommandParameter("Callback", title).Build());
            var useCase = new Lazy<IGetByIdCategoryUseCase>(() => GetByIdCategoryUseCaseBuilder.Instance().Build());

            var viewModel = new TimerTaskViewModel(navigation, useCase, timerUserTask) { Title = "" };

            Action action = () => viewModel.AddTaskTitleCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task Validade_Initialize_WithoutTitle(bool thereIsTimer)
        {
            var timerBuilder = TimerUserTaskBuilder.Instance();

            if (thereIsTimer)
                timerBuilder = timerBuilder.TimerRunning();

            Lazy<ITimerUserTask> timerUserTask = new Lazy<ITimerUserTask>(() => timerBuilder.Build());
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetByIdCategoryUseCase>(() => GetByIdCategoryUseCaseBuilder.Instance().Build());

            var viewModel = new TimerTaskViewModel(navigation, useCase, timerUserTask);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Subcategory", RequestSubcategory.Instance().Build())
                .Build();

            Func<Task> action = () => viewModel.InitializeAsync(parameters);

            await action.Should().NotThrowAsync();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task Validade_Initialize(bool thereIsTimer)
        {
            var timerBuilder = TimerUserTaskBuilder.Instance();

            if (thereIsTimer)
                timerBuilder = timerBuilder.TimerRunning();

            Lazy<ITimerUserTask> timerUserTask = new Lazy<ITimerUserTask>(() => timerBuilder.GetTitle().Build());
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetByIdCategoryUseCase>(() => GetByIdCategoryUseCaseBuilder.Instance().Build());

            var viewModel = new TimerTaskViewModel(navigation, useCase, timerUserTask);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Subcategory", RequestSubcategory.Instance().Build())
                .Build();

            Func<Task> action = () => viewModel.InitializeAsync(parameters);

            await action.Should().NotThrowAsync();
        }
    }
}
