using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.Services.BackGroundService;
using Timerom.App.ViewModels.Home;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Service;
using Xunit;

namespace ViewModels.Test.Home
{
    public class HomePageViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());

            var viewModel = new HomePageViewModel(navigation, timerUserTask);

            viewModel.HomeCommand.Should().NotBeNull();
            viewModel.AddTaskCommand.Should().NotBeNull();
            viewModel.StartTaskCommand.Should().NotBeNull();
            viewModel.ShowReportCommand.Should().NotBeNull();
            viewModel.ThereIsTimer.Should().BeFalse();
        }

        [Fact]
        public void Validade_Command_HomeCommand()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());

            var viewModel = new HomePageViewModel(navigation, timerUserTask);

            Action action = () => viewModel.HomeCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_AddTask()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());

            var viewModel = new HomePageViewModel(navigation, timerUserTask);

            Action action = () => viewModel.AddTaskCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_ShowReport()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());

            var viewModel = new HomePageViewModel(navigation, timerUserTask);

            Action action = () => viewModel.ShowReportCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Validade_Command_StartTask(bool thereIsTimer)
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            Lazy<ITimerUserTask> timerUserTask;

            if(thereIsTimer)
                timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());
            else
                timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().Build());

            var viewModel = new HomePageViewModel(navigation, timerUserTask);

            viewModel.OnNavigatedTo(null);

            Action action = () => viewModel.StartTaskCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_ShowCategories()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());

            var viewModel = new HomePageViewModel(navigation, timerUserTask);

            Action action = () => viewModel.ShowCategoriesCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_OnNavigatedFrom_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());

            var viewModel = new HomePageViewModel(navigation, timerUserTask);

            Action action = () => viewModel.OnNavigatedFrom(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_OnNavigatedTo_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());

            var viewModel = new HomePageViewModel(navigation, timerUserTask);

            Action action = () => viewModel.OnNavigatedTo(null);

            action.Should().NotThrow();
        }
    }
}
