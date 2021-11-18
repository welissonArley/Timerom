using FluentAssertions;
using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ViewModels.Tasks;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Tasks
{
    public class SelectSubCategoryForTaskViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<ILastTaskTimeForTodayUseCase>(() => LastTaskTimeForTodayUseCaseBuilder.Instance().Build());

            var viewModel = new SelectSubCategoryForTaskViewModel(useCase, navigation);

            viewModel.SearchTextChangedCommand.Should().NotBeNull();
            viewModel.ItemSelectedCommand.Should().NotBeNull();
        }

        [Fact]
        public async Task Validade_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<ILastTaskTimeForTodayUseCase>(() => LastTaskTimeForTodayUseCaseBuilder.Instance().Build());

            var viewModel = new SelectSubCategoryForTaskViewModel(useCase, navigation);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Option", OnSelectCategoryOptions.AddTask)
                .Parameter("Category", RequestCategory.Instance().Productive())
                .Build();

            Func<Task> action = async () => await viewModel.InitializeAsync(parameters);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Validade_Command_OnSearchText()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<ILastTaskTimeForTodayUseCase>(() => LastTaskTimeForTodayUseCaseBuilder.Instance().Build());

            var viewModel = new SelectSubCategoryForTaskViewModel(useCase, navigation);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Option", OnSelectCategoryOptions.AddTask)
                .Parameter("Category", RequestCategory.Instance().Productive())
                .Build();

            await viewModel.InitializeAsync(parameters);

            Action action = () => viewModel.SearchTextChangedCommand.Execute(viewModel.Category.Childrens.First().Name);
            action.Should().NotThrow();

            viewModel.Category.Childrens.Should().HaveCount(1);
        }

        [Fact]
        public async Task Validade_Command_ItemSelected_AddTask()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<ILastTaskTimeForTodayUseCase>(() => LastTaskTimeForTodayUseCaseBuilder.Instance().Build());

            var viewModel = new SelectSubCategoryForTaskViewModel(useCase, navigation);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Option", OnSelectCategoryOptions.AddTask)
                .Parameter("Category", RequestCategory.Instance().Productive())
                .Build();

            await viewModel.InitializeAsync(parameters);

            Action action = () => viewModel.ItemSelectedCommand.Execute(viewModel.Category);
            action.Should().NotThrow();
        }

        [Fact]
        public async Task Validade_Command_ItemSelected_Timer()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<ILastTaskTimeForTodayUseCase>(() => LastTaskTimeForTodayUseCaseBuilder.Instance().Build());

            var viewModel = new SelectSubCategoryForTaskViewModel(useCase, navigation);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Option", OnSelectCategoryOptions.Timer)
                .Parameter("Category", RequestCategory.Instance().Productive())
                .Build();

            await viewModel.InitializeAsync(parameters);

            Action action = () => viewModel.ItemSelectedCommand.Execute(viewModel.Category);
            action.Should().NotThrow();
        }
    }
}
