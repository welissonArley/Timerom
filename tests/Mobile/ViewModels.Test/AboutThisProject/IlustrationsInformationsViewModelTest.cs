using FluentAssertions;
using System;
using Timerom.App.Services.XamarinEssentials.Interface;
using Timerom.App.ViewModels.AboutThisProject;
using Useful.ToTests.Builders.Service;
using Xunit;

namespace ViewModels.Test.AboutThisProject
{
    public class IlustrationsInformationsViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var service = new Lazy<ILauncher>(() => LauncherBuilder.Instance().Build());

            var viewModel = new IlustrationsInformationsViewModel(service);

            viewModel.LinkCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_Command_Link()
        {
            var service = new Lazy<ILauncher>(() => LauncherBuilder.Instance().Build());

            var viewModel = new IlustrationsInformationsViewModel(service);

            Action action = () => viewModel.LinkCommand.Execute("https://example.com");

            action.Should().NotThrow();
        }
    }
}
