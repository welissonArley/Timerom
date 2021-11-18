using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ViewModels.Category;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Category
{
    public class AddUpdateCategoryViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertCategoryUseCase>(() => InsertCategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateCategoryUseCase>(() => UpdateCategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteCategoryUseCase>(() => DeleteCategoryUseCaseBuilder.Instance().Build());
            var canDeleteUseCase = new Lazy<ICanDeleteSubcategoryUseCase>(() => CanDeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateCategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase, canDeleteUseCase);

            viewModel.DeleteCommand.Should().NotBeNull();
            viewModel.SaveCommand.Should().NotBeNull();
            viewModel.AddSubCategoryCommand.Should().NotBeNull();
            viewModel.OptionCategoryCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_OnNavigatedTo_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertCategoryUseCase>(() => InsertCategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateCategoryUseCase>(() => UpdateCategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteCategoryUseCase>(() => DeleteCategoryUseCaseBuilder.Instance().Build());
            var canDeleteUseCase = new Lazy<ICanDeleteSubcategoryUseCase>(() => CanDeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateCategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase, canDeleteUseCase);

            StartViewModelToTest(viewModel);

            viewModel.Category.Should().NotBeNull();
        }

        [Fact]
        public void Validade_Command_Delete()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertCategoryUseCase>(() => InsertCategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateCategoryUseCase>(() => UpdateCategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteCategoryUseCase>(() => DeleteCategoryUseCaseBuilder.Instance().Build());
            var canDeleteUseCase = new Lazy<ICanDeleteSubcategoryUseCase>(() => CanDeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateCategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase, canDeleteUseCase);

            StartViewModelToTest(viewModel);

            Action action = () => viewModel.DeleteCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_Create()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertCategoryUseCase>(() => InsertCategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateCategoryUseCase>(() => UpdateCategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteCategoryUseCase>(() => DeleteCategoryUseCaseBuilder.Instance().Build());
            var canDeleteUseCase = new Lazy<ICanDeleteSubcategoryUseCase>(() => CanDeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateCategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase, canDeleteUseCase);

            StartViewModelToTest(viewModel);

            Action action = () => viewModel.SaveCommand.Execute(null);

            action.Should().NotThrow();

            var parameters = INavigationParametersBuilder.Instance().Build();
            action = () => viewModel.OnNavigatedFrom(parameters);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_Update()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertCategoryUseCase>(() => InsertCategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateCategoryUseCase>(() => UpdateCategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteCategoryUseCase>(() => DeleteCategoryUseCaseBuilder.Instance().Build());
            var canDeleteUseCase = new Lazy<ICanDeleteSubcategoryUseCase>(() => CanDeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateCategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase, canDeleteUseCase);

            StartViewModelToTest(viewModel, 1);

            Action action = () => viewModel.SaveCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validate_Command_AddSubCategory()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertCategoryUseCase>(() => InsertCategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateCategoryUseCase>(() => UpdateCategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteCategoryUseCase>(() => DeleteCategoryUseCaseBuilder.Instance().Build());
            var canDeleteUseCase = new Lazy<ICanDeleteSubcategoryUseCase>(() => CanDeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateCategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase, canDeleteUseCase)
            {
                SubCategoryName = "Adding subCategory"
            };

            StartViewModelToTest(viewModel);

            Action action = () => viewModel.AddSubCategoryCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_OptionCategory()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertCategoryUseCase>(() => InsertCategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateCategoryUseCase>(() => UpdateCategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteCategoryUseCase>(() => DeleteCategoryUseCaseBuilder.Instance().Build());
            var canDeleteUseCase = new Lazy<ICanDeleteSubcategoryUseCase>(() => CanDeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateCategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase, canDeleteUseCase);

            StartViewModelToTest(viewModel);

            Action action = () => viewModel.OptionCategoryCommand.Execute(viewModel.Category);

            action.Should().NotThrow();
        }

        private void StartViewModelToTest(AddUpdateCategoryViewModel viewModel, long? id = null)
        {
            var request = RequestCategory.Instance().Build();
            if (id.HasValue)
                request.Id = id.Value;

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Category", request)
                .Build();

            viewModel.OnNavigatedTo(parameters);
        }
    }
}
