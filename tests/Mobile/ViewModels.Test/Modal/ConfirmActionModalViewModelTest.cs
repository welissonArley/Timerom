using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.ViewModels.Modal;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.ExtensionMock;
using Useful.ToTests.Helper;
using Xamarin.CommunityToolkit.ObjectModel;
using Xunit;

namespace ViewModels.Test.Modal
{
    public class ConfirmActionModalViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new ConfirmActionModalViewModel(navigation);

            viewModel.CloseModalCommand.Should().NotBeNull();
            viewModel.IamSureCommand.Should().NotBeNull();
            viewModel.Title.Should().BeNullOrEmpty();
            viewModel.Description.Should().BeNullOrEmpty();
            viewModel.Action.Should().BeNull();
        }

        [Fact]
        public void Validade_OnNavigatedFrom()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new ConfirmActionModalViewModel(navigation);

            Action action = () => viewModel.OnNavigatedFrom(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_CloseModal()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            ReplaceExtensionMethodToFake.Replace(typeof(PopupExtensions), typeof(TimeromPopupExtensionsMock));

            var viewModel = new ConfirmActionModalViewModel(navigation);

            Action action = () => viewModel.CloseModalCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_IamSure()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            ReplaceExtensionMethodToFake.Replace(typeof(PopupExtensions), typeof(TimeromPopupExtensionsMock));

            var viewModel = new ConfirmActionModalViewModel(navigation);

            Action action = () => viewModel.IamSureCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_OnNavigatedTo()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new ConfirmActionModalViewModel(navigation);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Title", "Title test")
                .Parameter("Description", "Description test")
                .Parameter("Action", new AsyncCommand(() => { return null; }))
                .Build();

            Action action = () => viewModel.OnNavigatedTo(parameters);

            action.Should().NotThrow();

            viewModel.Title.Should().NotBeNullOrWhiteSpace();
            viewModel.Description.Should().NotBeNullOrWhiteSpace();
            viewModel.Action.Should().NotBeNull();
        }
    }
}
