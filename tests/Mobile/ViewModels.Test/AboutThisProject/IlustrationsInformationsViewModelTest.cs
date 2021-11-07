using FluentAssertions;
using Timerom.App.ViewModels.AboutThisProject;
using Xunit;

namespace ViewModels.Test.AboutThisProject
{
    public class IlustrationsInformationsViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var useCase = new IlustrationsInformationsViewModel();

            useCase.LinkCommand.Should().NotBeNull();
        }
    }
}
