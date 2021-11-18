using FluentAssertions;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.App.ViewModels.Tasks;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCase;
using Xamarin.CommunityToolkit.ObjectModel;
using Xunit;

namespace ViewModels.Test.Tasks
{
    public class TaskDetailsViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase= new Lazy<IGetAllUserTaskUseCase>(() => GetAllUserTaskUseCaseBuilder.Instance().Build());

            var viewModel = new TaskDetailsViewModel(navigation, useCase);

            viewModel.SearchTextChangedCommand.Should().NotBeNull();
            viewModel.DateChangedCommand.Should().NotBeNull();
            viewModel.SelectedItemCommand.Should().NotBeNull();
            viewModel.FilterListCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_OnNavigatedFrom()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllUserTaskUseCase>(() => GetAllUserTaskUseCaseBuilder.Instance().Build());

            var viewModel = new TaskDetailsViewModel(navigation, useCase);

            Action action = () => viewModel.OnNavigatedFrom(null);

            action.Should().NotThrow();
        }

        [Fact]
        public async Task Validade_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllUserTaskUseCase>(() => GetAllUserTaskUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new TaskDetailsViewModel(navigation, useCase);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Date", DateTime.Now)
                .Parameter("CallbackUpdateUserTask", new AsyncCommand(() => { return null; }))
                .Build();

            Func<Task> action = async() => await viewModel.InitializeAsync(parameters);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Validade_Initialize_CategoriesFilter()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllUserTaskUseCase>(() => GetAllUserTaskUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new TaskDetailsViewModel(navigation, useCase);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("CategoriesFilter", new List<long> { 1 })
                .Parameter("Date", DateTime.Now)
                .Parameter("CallbackUpdateUserTask", new AsyncCommand(() => { return null; }))
                .Build();

            Func<Task> action = async () => await viewModel.InitializeAsync(parameters);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public void Validade_OnNavigatedTo()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllUserTaskUseCase>(() => GetAllUserTaskUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new TaskDetailsViewModel(navigation, useCase)
            {
                Model = RequestTasksDetailsModel.Instance().Build()
            };

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Refresh", 1)
                .Build();

            Action action = () => viewModel.OnNavigatedTo(parameters);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_SelectedItem()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllUserTaskUseCase>(() => GetAllUserTaskUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new TaskDetailsViewModel(navigation, useCase)
            {
                Model = RequestTasksDetailsModel.Instance().Build()
            };

            Action action = () => viewModel.SelectedItemCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_FilterList()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllUserTaskUseCase>(() => GetAllUserTaskUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new TaskDetailsViewModel(navigation, useCase)
            {
                Model = RequestTasksDetailsModel.Instance().Build()
            };

            Action action = () => viewModel.FilterListCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_DateChanged()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllUserTaskUseCase>(() => GetAllUserTaskUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new TaskDetailsViewModel(navigation, useCase)
            {
                Model = RequestTasksDetailsModel.Instance().Build()
            };

            Action action = () => viewModel.DateChangedCommand.Execute(DateTime.UtcNow);

            action.Should().NotThrow();
        }

        [Fact]
        public async Task Validade_Command_OnSearchText()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IGetAllUserTaskUseCase>(() => GetAllUserTaskUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new TaskDetailsViewModel(navigation, useCase);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Date", DateTime.Now)
                .Parameter("CallbackUpdateUserTask", new AsyncCommand(() => { return null; }))
                .Build();

            await viewModel.InitializeAsync(parameters);

            Action action = () => viewModel.SearchTextChangedCommand.Execute(viewModel.Model.Tasks.First().Title);

            action.Should().NotThrow();
            viewModel.Model.Tasks.Should().HaveCount(1);
        }
    }
}
