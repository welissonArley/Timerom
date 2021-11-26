using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.ViewModels.Tasks;
using Useful.ToTests.Builders.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
using Xunit;

namespace ViewModels.Test.Tasks
{
    public class TitleTaskViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new TitleTaskViewModel(navigation);

            viewModel.SaveCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_Command_Save()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new TitleTaskViewModel(navigation);

            Action action = () => viewModel.SaveCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new TitleTaskViewModel(navigation);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Title", "Title test")
                .Parameter("Callback", new AsyncCommand<string>((value) => { return null; }))
                .Build();

            Action action = () => viewModel.Initialize(parameters);

            action.Should().NotThrow();
            viewModel.TitleTask.Should().Be("Title test");
        }
    }
}
