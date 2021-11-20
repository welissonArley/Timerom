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
    public class AddUpdateSubcategoryViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertSubcategoryUseCase>(() => InsertSubcategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateSubcategoryUseCase>(() => UpdateSubcategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteSubcategoryUseCase>(() => DeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateSubcategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase);

            viewModel.DeleteCommand.Should().NotBeNull();
            viewModel.SaveCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_OnNavigatedTo_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertSubcategoryUseCase>(() => InsertSubcategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateSubcategoryUseCase>(() => UpdateSubcategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteSubcategoryUseCase>(() => DeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateSubcategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase);

            StartViewModelToTest(viewModel);

            viewModel.Category.Should().NotBeNull();
            viewModel.SubCategory.Should().NotBeNull();
        }

        [Fact]
        public void Validade_Command_Delete()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertSubcategoryUseCase>(() => InsertSubcategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateSubcategoryUseCase>(() => UpdateSubcategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteSubcategoryUseCase>(() => DeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateSubcategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase);

            StartViewModelToTest(viewModel);

            Action action = () => viewModel.DeleteCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_Create()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var subCategory = RequestSubcategory.Instance().Build();
            var createUseCase = new Lazy<IInsertSubcategoryUseCase>(() => InsertSubcategoryUseCaseBuilder.Instance().Execute(subCategory).Build());
            var updateUseCase = new Lazy<IUpdateSubcategoryUseCase>(() => UpdateSubcategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteSubcategoryUseCase>(() => DeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateSubcategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase);

            StartViewModelToTest(viewModel);

            Action action = () => viewModel.SaveCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_Update()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertSubcategoryUseCase>(() => InsertSubcategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateSubcategoryUseCase>(() => UpdateSubcategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteSubcategoryUseCase>(() => DeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateSubcategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase);

            StartViewModelToTest(viewModel, 1);

            Action action = () => viewModel.SaveCommand.Execute(null);

            action.Should().NotThrow();

            var parameters = INavigationParametersBuilder.Instance().Build();
            action = () => viewModel.OnNavigatedFrom(parameters);

            action.Should().NotThrow();
        }

        private void StartViewModelToTest(AddUpdateSubcategoryViewModel viewModel, long? id = null)
        {
            var category = RequestCategory.Instance().Build();
            
            var subCategory = RequestSubcategory.Instance().Build();
            if (id.HasValue)
                subCategory.Id = id.Value;

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Category", category)
                .Parameter("SubCategory", subCategory)
                .Build();

            viewModel.OnNavigatedTo(parameters);
        }

        [Fact]
        public async Task Validate_Callback_TaskDetailsPage()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().ExecuteCommandParameterFromModal("Action").Build());

            var createUseCase = new Lazy<IInsertSubcategoryUseCase>(() => InsertSubcategoryUseCaseBuilder.Instance().Build());
            var updateUseCase = new Lazy<IUpdateSubcategoryUseCase>(() => UpdateSubcategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteSubcategoryUseCase>(() => DeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateSubcategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase);

            StartViewModelToTest(viewModel, 1);

            Func<Task> action = async () => await viewModel.DeleteCommand.ExecuteAsync();

            await action.Should().NotThrowAsync();

            var parameters = INavigationParametersBuilder.Instance().Build();
            Action actionNavigateFrom = () => viewModel.OnNavigatedFrom(parameters);

            actionNavigateFrom.Should().NotThrow();
        }
    }
}
