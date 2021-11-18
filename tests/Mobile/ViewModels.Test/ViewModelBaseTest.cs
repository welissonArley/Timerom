using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ViewModels.Category;
using Timerom.App.ViewModels.Reports.ParetoPrinciple;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test
{
    public class ViewModelBaseTest
    {
        [Fact]
        public void Validade_Command_Create_Error()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertCategoryUseCase>(() => InsertCategoryUseCaseBuilder.Instance().TimeromError().Build());
            var updateUseCase = new Lazy<IUpdateCategoryUseCase>(() => UpdateCategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteCategoryUseCase>(() => DeleteCategoryUseCaseBuilder.Instance().Build());
            var canDeleteUseCase = new Lazy<ICanDeleteSubcategoryUseCase>(() => CanDeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateCategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase, canDeleteUseCase)
            {
                Category = new Timerom.App.Model.Category()
            };

            Action action = () => viewModel.SaveCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_Create_ErrorValidation()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());

            var createUseCase = new Lazy<IInsertCategoryUseCase>(() => InsertCategoryUseCaseBuilder.Instance().ValidationError().Build());
            var updateUseCase = new Lazy<IUpdateCategoryUseCase>(() => UpdateCategoryUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteCategoryUseCase>(() => DeleteCategoryUseCaseBuilder.Instance().Build());
            var canDeleteUseCase = new Lazy<ICanDeleteSubcategoryUseCase>(() => CanDeleteSubcategoryUseCaseBuilder.Instance().Build());

            var viewModel = new AddUpdateCategoryViewModel(navigation, createUseCase, updateUseCase, deleteUseCase, canDeleteUseCase)
            {
                Category = new Timerom.App.Model.Category()
            };

            Action action = () => viewModel.SaveCommand.Execute(null);

            action.Should().NotThrow();
        }
    }
}
