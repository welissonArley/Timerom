using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ViewModels.Reports.ParetoPrinciple;
using Useful.ToTests.Builders.Navigation;
using Xunit;

namespace ViewModels.Test.Reports.ParetoPrinciple
{
    public class ChooseDatesParetoPrincipleViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new ChooseDatesParetoPrincipleViewModel(navigation);

            viewModel.Option.Should().Equals(SelectDateParetoPrincipleOptions.Last7Days);
            viewModel.OptionCommand.Should().NotBeNull();
            viewModel.ExecuteCommand.Should().NotBeNull();
            viewModel.WhatIsParetoPrincipleCommand.Should().NotBeNull();
            viewModel.StartsAt.Should().Equals(DateTime.Today.AddDays(-7));
            viewModel.EndsAt.Should().Equals(DateTime.Today);
        }

        [Fact]
        public void Validade_Command_WhatIsParetoPrinciple()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new ChooseDatesParetoPrincipleViewModel(navigation);

            Action action = () => viewModel.WhatIsParetoPrincipleCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_Execute()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new ChooseDatesParetoPrincipleViewModel(navigation);

            Action action = () => viewModel.ExecuteCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(SelectDateParetoPrincipleOptions.CustomDates)]
        [InlineData(SelectDateParetoPrincipleOptions.Last15Days)]
        [InlineData(SelectDateParetoPrincipleOptions.Last7Days)]
        [InlineData(SelectDateParetoPrincipleOptions.LastMonth)]
        [InlineData(SelectDateParetoPrincipleOptions.ThisMonth)]
        public void Validade_Command_Option(SelectDateParetoPrincipleOptions option)
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new ChooseDatesParetoPrincipleViewModel(navigation);

            Action action = () => viewModel.OptionCommand.Execute(option);

            action.Should().NotThrow();
        }
    }
}
