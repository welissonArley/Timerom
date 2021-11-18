﻿using FluentAssertions;
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
    }
}
