using FluentAssertions;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ViewModels.Category;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Category
{
    public class SelectCategoryToUpdateViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Build());

            var viewModel = new SelectCategoryToUpdateViewModel(getAllUseCase, navigation);

            viewModel.SearchTextChangedCommand.Should().NotBeNull();
            viewModel.ItemSelectedCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_Command_ItemSelected()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Build());

            var viewModel = new SelectCategoryToUpdateViewModel(getAllUseCase, navigation);

            var category = RequestCategory.Instance().Build();
            Action action = () => viewModel.ItemSelectedCommand.Execute(category);

            action.Should().NotThrow();
        }

        [Fact]
        public async Task Validade_Error_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Build());

            var viewModel = new SelectCategoryToUpdateViewModel(getAllUseCase, navigation);

            Func<Task> action = async () => await viewModel.InitializeAsync(null);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Validade_Sucess_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new SelectCategoryToUpdateViewModel(getAllUseCase, navigation);

            Func<Task> action = async () => await viewModel.InitializeAsync(null);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Validade_Command_OnSearchText()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new SelectCategoryToUpdateViewModel(getAllUseCase, navigation);
            await viewModel.InitializeAsync(null);

            Action action = () => viewModel.SearchTextChangedCommand.Execute("");

            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(CategoriesInlineDataTest))]
        public async Task Validade_OnNavigatedTo_Updated(Timerom.App.Model.Category category)
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new SelectCategoryToUpdateViewModel(getAllUseCase, navigation);
            await viewModel.InitializeAsync(null);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Updated", category)
                .Build();

            Action action = () => viewModel.OnNavigatedTo(parameters);

            action.Should().NotThrow();

            action = () => viewModel.OnNavigatedFrom(parameters);

            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(CategoriesInlineDataTest))]
        public async Task Validade_OnNavigatedTo_Deleted(Timerom.App.Model.Category category)
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new SelectCategoryToUpdateViewModel(getAllUseCase, navigation);
            await viewModel.InitializeAsync(null);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Deleted", category)
                .Build();

            Action action = () => viewModel.OnNavigatedTo(parameters);

            action.Should().NotThrow();

            action = () => viewModel.OnNavigatedFrom(parameters);

            action.Should().NotThrow();
        }
    }
}
