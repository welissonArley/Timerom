using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.Services.BackGroundService;
using Timerom.App.ViewModels.Modal.MenuOptions;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Service;
using Useful.ToTests.ExtensionMock;
using Useful.ToTests.Helper;
using Xunit;

namespace ViewModels.Test.Modal.MenuOptions
{
    public class FloatActionUserTaskViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new FloatActionUserTaskViewModel(navigation, timerUserTask);

            viewModel.AddUserTaskCommand.Should().NotBeNull();
            viewModel.StartUserTaskCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_Command_AddUserTask()
        {
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());

            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            ReplaceExtensionMethodToFake.Replace(typeof(PopupExtensions), typeof(TimeromPopupExtensionsMock));

            var viewModel = new FloatActionUserTaskViewModel(navigation, timerUserTask);

            Action action = () => viewModel.AddUserTaskCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Validade_Command_StartUserTask(bool thereIsTimer)
        {
            Lazy<ITimerUserTask> timerUserTask;

            if (thereIsTimer)
                timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());
            else
                timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().Build());

            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            ReplaceExtensionMethodToFake.Replace(typeof(PopupExtensions), typeof(TimeromPopupExtensionsMock));

            var viewModel = new FloatActionUserTaskViewModel(navigation, timerUserTask);

            viewModel.Initialize(null);

            Action action = () => viewModel.StartUserTaskCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var timerUserTask = new Lazy<ITimerUserTask>(() => TimerUserTaskBuilder.Instance().TimerRunning().Build());

            var viewModel = new FloatActionUserTaskViewModel(navigation, timerUserTask);

            Action action = () => viewModel.Initialize(null);

            action.Should().NotThrow();
        }
    }
}
