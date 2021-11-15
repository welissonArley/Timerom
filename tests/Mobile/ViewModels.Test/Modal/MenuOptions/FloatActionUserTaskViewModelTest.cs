using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.ViewModels.Modal.MenuOptions;
using Useful.ToTests.Builders.Navigation;
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
    }
}
