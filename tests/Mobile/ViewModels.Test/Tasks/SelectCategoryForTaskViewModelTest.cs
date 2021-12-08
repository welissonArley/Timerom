using FluentAssertions;
using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ViewModels.Tasks;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Tasks
{
    public class SelectCategoryForTaskViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Build());

            var viewModel = new SelectCategoryForTaskViewModel(useCase, navigation);

            viewModel.SearchTextChangedCommand.Should().NotBeNull();
            viewModel.ItemSelectedCommand.Should().NotBeNull();
        }

        [Fact]
        public async Task Validade_Initialize_Success()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new SelectCategoryForTaskViewModel(useCase, navigation);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Option", OnSelectCategoryOptions.AddTask)
                .Build();

            Func<Task> action = async () => await viewModel.InitializeAsync(parameters);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Validade_Initialize_Error()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Build());

            var viewModel = new SelectCategoryForTaskViewModel(useCase, navigation);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Option", OnSelectCategoryOptions.AddTask)
                .Build();

            Func<Task> action = async () => await viewModel.InitializeAsync(parameters);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public void Validade_Command_DeleteCommand()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Build());

            var viewModel = new SelectCategoryForTaskViewModel(useCase, navigation);

            Action action = () => viewModel.ItemSelectedCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public async Task Validade_Command_OnSearchText()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new SelectCategoryForTaskViewModel(useCase, navigation);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Option", OnSelectCategoryOptions.AddTask)
                .Build();

            await viewModel.InitializeAsync(parameters);

            Action action = () => viewModel.SearchTextChangedCommand.Execute(viewModel.ProductiveCategories.First().Name);

            action.Should().NotThrow();
            viewModel.ProductiveCategories.Should().HaveCount(1);
            viewModel.NeutralCategories.Should().HaveCount(0);
            viewModel.UnproductiveCategories.Should().HaveCount(0);
        }
    }
}
