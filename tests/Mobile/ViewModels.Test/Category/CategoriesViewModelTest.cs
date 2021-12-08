using FluentAssertions;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ViewModels.Category;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Category
{
    public class CategoriesViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Build());

            var viewModel = new CategoriesViewModel(getAllUseCase, navigation);

            viewModel.FloatActionCommand.Should().NotBeNull();
            viewModel.AddSubCategoryCommand.Should().NotBeNull();
            viewModel.UpdateSubCategoryCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_Command_FloatAction()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Build());

            var viewModel = new CategoriesViewModel(getAllUseCase, navigation);

            Action action = () => viewModel.FloatActionCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_AddSubCategory()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Build());

            var viewModel = new CategoriesViewModel(getAllUseCase, navigation);

            var category = RequestCategory.Instance().Build();

            Action action = () => viewModel.AddSubCategoryCommand.Execute(category);

            action.Should().NotThrow();
        }

        [Fact]
        public async Task Validade_Error_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Build());

            var viewModel = new CategoriesViewModel(getAllUseCase, navigation);

            Func<Task> action = async () => await viewModel.InitializeAsync(null);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Validade_Sucess_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new CategoriesViewModel(getAllUseCase, navigation);

            Func<Task> action = async () => await viewModel.InitializeAsync(null);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Validade_Command_UpdateSubCategory()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new CategoriesViewModel(getAllUseCase, navigation);
            await viewModel.InitializeAsync(null);

            var category = RequestCategory.Instance().Build();

            Action action = () => viewModel.UpdateSubCategoryCommand.Execute(category.Childrens.First());

            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(CategoriesInlineDataTest))]
        public async Task Validade_OnNavigatedTo_NewCategoriesCreated(Timerom.App.Model.Category category)
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new CategoriesViewModel(getAllUseCase, navigation);
            await viewModel.InitializeAsync(null);
            
            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Created", new List<Timerom.App.Model.Category> { category })
                .Build();

            Action action = () => viewModel.OnNavigatedTo(parameters);

            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(CategoriesInlineDataTest))]
        public async Task Validade_OnNavigatedTo_UpdateCategories(Timerom.App.Model.Category category)
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new CategoriesViewModel(getAllUseCase, navigation);
            await viewModel.InitializeAsync(null);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Updated", new List<Timerom.App.Model.Category> { category })
                .Build();

            Action action = () => viewModel.OnNavigatedTo(parameters);

            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(CategoriesInlineDataTest))]
        public async Task Validade_OnNavigatedTo_DeletedCategories(Timerom.App.Model.Category category)
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new CategoriesViewModel(getAllUseCase, navigation);
            await viewModel.InitializeAsync(null);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Deleted", new List<Timerom.App.Model.Category> { category })
                .Build();

            Action action = () => viewModel.OnNavigatedTo(parameters);

            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(CategoryType.Productive)]
        [InlineData(CategoryType.Neutral)]
        [InlineData(CategoryType.Unproductive)]
        public async Task Validade_OnNavigatedTo_UpdateList(CategoryType categoryType)
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new CategoriesViewModel(getAllUseCase, navigation);
            await viewModel.InitializeAsync(null);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("UpdateList", categoryType)
                .Build();

            Action action = () => viewModel.OnNavigatedTo(parameters);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Sucess_OnNavigatedFrom()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var getAllUseCase = new Lazy<IGetAllCategoriesUseCase>(() => GetAllCategoriesUseCaseBuilder.Instance().Categories().Build());

            var viewModel = new CategoriesViewModel(getAllUseCase, navigation);

            Action action = () => viewModel.OnNavigatedFrom(null);

            action.Should().NotThrow();
        }
    }
}
