using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ViewModels.Modal.MenuOptions;
using Useful.ToTests.Builders.Navigation;
using Xunit;

namespace ViewModels.Test.Modal.MenuOptions
{
    public class FloatActionCategoriesViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new FloatActionCategoriesViewModel(navigation);

            viewModel.AddCategoryCommand.Should().NotBeNull();
            viewModel.UpdateCategoryCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_Command_UpdateCategory()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new FloatActionCategoriesViewModel(navigation);

            Action action = () => viewModel.UpdateCategoryCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(CategoryType.Neutral)]
        [InlineData(CategoryType.Productive)]
        [InlineData(CategoryType.Unproductive)]
        public void Validade_Command_AddCategory(CategoryType type)
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var viewModel = new FloatActionCategoriesViewModel(navigation);

            Action action = () => viewModel.AddCategoryCommand.Execute(type);

            action.Should().NotThrow();
        }
    }
}
