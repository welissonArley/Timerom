using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.ViewModels.Modal.MenuOptions;
using Useful.ToTests.Builders.Navigation;
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
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new FloatActionUserTaskViewModel(navigation);

            viewModel.AddUserTaskCommand.Should().NotBeNull();
            viewModel.StartUserTaskCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_Command_AddUserTask()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            ReplaceExtensionMethodToFake.Replace(typeof(PopupExtensions), typeof(TimeromPopupExtensionsMock));

            var viewModel = new FloatActionUserTaskViewModel(navigation);

            Action action = () => viewModel.AddUserTaskCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_StartUserTask()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            ReplaceExtensionMethodToFake.Replace(typeof(PopupExtensions), typeof(TimeromPopupExtensionsMock));

            var viewModel = new FloatActionUserTaskViewModel(navigation);

            Action action = () => viewModel.StartUserTaskCommand.Execute(null);

            action.Should().NotThrow();
        }
    }
}
