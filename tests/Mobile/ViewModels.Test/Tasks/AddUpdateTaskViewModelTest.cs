using FluentAssertions;
using Prism.Navigation;
using System;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.App.ViewModels.Tasks;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Tasks
{
    public class AddUpdateTaskViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var insertTaskUseCase = new Lazy<IInsertTaskUseCase>(() => InsertTaskUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteUserTaskUseCase>(() => DeleteUserTaskUseCaseBuilder.Instance().Build());
            var updateTaskUseCase = new Lazy<IUpdateUserTaskUseCase>(() => UpdateUserTaskUseCase.Instance().Build());

            var viewModel = new AddUpdateTaskViewModel(insertTaskUseCase, deleteUseCase, updateTaskUseCase, navigation);

            viewModel.SaveCommand.Should().NotBeNull();
            viewModel.DeleteCommand.Should().NotBeNull();

            viewModel.TotalTime.TotalHours.Should().Equals(0);
            viewModel.TimeStartsAt.TotalHours.Should().Equals(0);
            viewModel.TimeEndsAt.TotalHours.Should().Equals(0);
            viewModel.DateStartsAt.Date.Should().Equals(DateTime.Now.Date);
            viewModel.DateEndsAt.Date.Should().Equals(DateTime.Now.Date);
        }

        [Fact]
        public void Validade_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var insertTaskUseCase = new Lazy<IInsertTaskUseCase>(() => InsertTaskUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteUserTaskUseCase>(() => DeleteUserTaskUseCaseBuilder.Instance().Build());
            var updateTaskUseCase = new Lazy<IUpdateUserTaskUseCase>(() => UpdateUserTaskUseCase.Instance().Build());

            var viewModel = new AddUpdateTaskViewModel(insertTaskUseCase, deleteUseCase, updateTaskUseCase, navigation);

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Task", RequestTask.Instance().Build())
                .Build();

            Action action = () => viewModel.Initialize(parameters);

            action.Should().NotThrow();
            viewModel.TotalTime.TotalHours.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Validade_Command_DeleteCommand()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var insertTaskUseCase = new Lazy<IInsertTaskUseCase>(() => InsertTaskUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteUserTaskUseCase>(() => DeleteUserTaskUseCaseBuilder.Instance().Build());
            var updateTaskUseCase = new Lazy<IUpdateUserTaskUseCase>(() => UpdateUserTaskUseCase.Instance().Build());

            var viewModel = new AddUpdateTaskViewModel(insertTaskUseCase, deleteUseCase, updateTaskUseCase, navigation)
            {
                Task = RequestTask.Instance().Build()
            };

            Action action = () => viewModel.DeleteCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_Save_CreateTask()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var insertTaskUseCase = new Lazy<IInsertTaskUseCase>(() => InsertTaskUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteUserTaskUseCase>(() => DeleteUserTaskUseCaseBuilder.Instance().Build());
            var updateTaskUseCase = new Lazy<IUpdateUserTaskUseCase>(() => UpdateUserTaskUseCase.Instance().Build());

            var viewModel = new AddUpdateTaskViewModel(insertTaskUseCase, deleteUseCase, updateTaskUseCase, navigation)
            {
                Task = RequestTask.Instance().Build()
            };

            Action action = () => viewModel.SaveCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_Save_UpdateTask()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var insertTaskUseCase = new Lazy<IInsertTaskUseCase>(() => InsertTaskUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteUserTaskUseCase>(() => DeleteUserTaskUseCaseBuilder.Instance().Build());
            var updateTaskUseCase = new Lazy<IUpdateUserTaskUseCase>(() => UpdateUserTaskUseCase.Instance().Build());

            var task = RequestTask.Instance().Build();
            task.Id = 1;

            var viewModel = new AddUpdateTaskViewModel(insertTaskUseCase, deleteUseCase, updateTaskUseCase, navigation)
            {
                Task = task
            };

            Action action = () => viewModel.SaveCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validate_Callback_DeleteTask()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().ExecuteCommandParameterFromModal("Action").Build());

            var insertTaskUseCase = new Lazy<IInsertTaskUseCase>(() => InsertTaskUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteUserTaskUseCase>(() => DeleteUserTaskUseCaseBuilder.Instance().Build());
            var updateTaskUseCase = new Lazy<IUpdateUserTaskUseCase>(() => UpdateUserTaskUseCase.Instance().Build());

            var viewModel = new AddUpdateTaskViewModel(insertTaskUseCase, deleteUseCase, updateTaskUseCase, navigation)
            {
                Task = RequestTask.Instance().Build()
            };

            Action action = () => viewModel.DeleteCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validate_Dates()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var insertTaskUseCase = new Lazy<IInsertTaskUseCase>(() => InsertTaskUseCaseBuilder.Instance().Build());
            var deleteUseCase = new Lazy<IDeleteUserTaskUseCase>(() => DeleteUserTaskUseCaseBuilder.Instance().Build());
            var updateTaskUseCase = new Lazy<IUpdateUserTaskUseCase>(() => UpdateUserTaskUseCase.Instance().Build());

            var viewModel = new AddUpdateTaskViewModel(insertTaskUseCase, deleteUseCase, updateTaskUseCase, navigation)
            {
                Task = RequestTask.Instance().Build()
            };

            viewModel.TimeStartsAt = new TimeSpan(1,0,0);
            viewModel.TimeEndsAt = new TimeSpan(2,0,0);
            viewModel.DateStartsAt = DateTime.Today;
            viewModel.DateEndsAt = DateTime.Today;

            viewModel.TotalTime.TotalHours.Should().BeGreaterThan(0);
            viewModel.TimeStartsAt.TotalHours.Should().BeGreaterThan(0);
            viewModel.TimeEndsAt.TotalHours.Should().BeGreaterThan(0);
            viewModel.DateStartsAt.Date.Should().Equals(DateTime.Today);
            viewModel.DateEndsAt.Date.Should().Equals(DateTime.Today);
        }
    }
}
