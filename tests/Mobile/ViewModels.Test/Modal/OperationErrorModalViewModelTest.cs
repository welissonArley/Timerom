using FluentAssertions;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using Timerom.App.ViewModels.Modal;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.ExtensionMock;
using Useful.ToTests.Helper;
using Xunit;

namespace ViewModels.Test.Modal
{
    public class OperationErrorModalViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new OperationErrorModalViewModel(navigation);

            viewModel.CloseModalCommand.Should().NotBeNull();
            viewModel.Messages.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Validade_OnNavigatedFrom()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new OperationErrorModalViewModel(navigation);

            Action action = () => viewModel.OnNavigatedFrom(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_CloseModal()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            ReplaceExtensionMethodToFake.Replace(typeof(PopupExtensions), typeof(TimeromPopupExtensionsMock));

            var viewModel = new OperationErrorModalViewModel(navigation);

            Action action = () => viewModel.CloseModalCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_OnNavigatedTo()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new OperationErrorModalViewModel(navigation);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Messages", new List<string>{ "Title test"})
                .Build();

            Action action = () => viewModel.OnNavigatedTo(parameters);

            action.Should().NotThrow();

            viewModel.Messages.Should().NotBeNullOrEmpty();
        }
    }
}
