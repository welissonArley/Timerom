using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Tasks
{
    public class AddUpdateTaskViewModel : ViewModelBase, IInitialize
    {
        private readonly Lazy<IUpdateUserTaskUseCase> updateTaskUseCase;
        private readonly Lazy<IInsertTaskUseCase> insertTaskUseCase;
        private readonly Lazy<IDeleteUserTaskUseCase> deleteUseCase;
        private IInsertTaskUseCase _insertTaskUseCase => insertTaskUseCase.Value;
        private IDeleteUserTaskUseCase _deleteUseCase => deleteUseCase.Value;
        private IUpdateUserTaskUseCase _updateTaskUseCase => updateTaskUseCase.Value;

        public TaskModel Task { get; set; }
        public TimeSpan TotalTime => Task == null ? new TimeSpan() : (Task.EndsAt - Task.StartsAt);

        public TimeSpan TimeStartsAt
        {
            get => Task == null ? new TimeSpan() : new TimeSpan(Task.StartsAt.Hour, Task.StartsAt.Minute, 0);
            set { Task.StartsAt = Task.StartsAt.Date + value; RaisePropertyChanged("TotalTime"); }
        }
        public TimeSpan TimeEndsAt
        {
            get => Task == null ? new TimeSpan() : new TimeSpan(Task.EndsAt.Hour, Task.EndsAt.Minute, 0);
            set { Task.EndsAt = Task.EndsAt.Date + value; RaisePropertyChanged("TotalTime"); }
        }
        public DateTime DateStartsAt
        {
            get => Task == null ? DateTime.Now : new DateTime(Task.StartsAt.Year, Task.StartsAt.Month, Task.StartsAt.Day);
            set { Task.StartsAt = value.Date + TimeStartsAt; RaisePropertyChanged("DateStartsAt"); RaisePropertyChanged("DateEndsAt"); RaisePropertyChanged("TotalTime"); }
        }
        public DateTime DateEndsAt
        {
            get => Task == null ? DateTime.Now : new DateTime(Task.EndsAt.Year, Task.EndsAt.Month, Task.EndsAt.Day);
            set { Task.EndsAt = value.Date + TimeEndsAt; RaisePropertyChanged("DateStartsAt"); RaisePropertyChanged("DateEndsAt"); RaisePropertyChanged("TotalTime"); }
        }

        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand DeleteCommand { get; private set; }

        public AddUpdateTaskViewModel(Lazy<IInsertTaskUseCase> insertTaskUseCase, Lazy<IDeleteUserTaskUseCase> deleteUseCase,
            Lazy<IUpdateUserTaskUseCase> updateTaskUseCase, Lazy<INavigationService> navigationService) : base(navigationService)
        {
            this.insertTaskUseCase = insertTaskUseCase;
            this.deleteUseCase = deleteUseCase;
            this.updateTaskUseCase = updateTaskUseCase;

            SaveCommand = new AsyncCommand(SaveCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
            DeleteCommand = new AsyncCommand(DeleteCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
        }

        private async Task SaveCommandExecuted()
        {
            SavingStatus();

            if (Task.Id == 0)
                await CreateTask();
            else
                await UpdateTask();
        }

        private async Task CreateTask()
        {
            TrackEvent("AddUpdateTaskPage", "Create", EventFlag.Click);

            await _insertTaskUseCase.Execute(Task);

            await SucessStatus();

            var navParameters = new NavigationParameters
            {
                { "Refresh", 1 }
            };

            await _navigationService.GoBackToRootAsync(navParameters);
        }

        private async Task UpdateTask()
        {
            TrackEvent("AddUpdateTaskPage", "Update", EventFlag.Click);

            await _updateTaskUseCase.Execute(Task);

            await SucessStatus();

            var navParameters = new NavigationParameters
            {
                { "Refresh", 1 }
            };

            await _navigationService.GoBackAsync(navParameters);
        }

        private async Task DeleteCommandExecuted()
        {
            NavigationParameters navParameters = new NavigationParameters
            {
                { "Title", ResourceText.TITLE_DELETE_TASK },
                { "Description", string.Format(ResourceText.DESCRIPTION_DELETE_TASK, Task.Title) },
                { "Action", new AsyncCommand(DeleteTask, allowsMultipleExecutions: false, onException: HandleException) }
            };

            await _navigationService.NavigateAsync(nameof(Views.Modal.ConfirmActionModal), navParameters, useModalNavigation: true);
        }

        private async Task DeleteTask()
        {
            TrackEvent("AddUpdateTaskPage", "Delete", EventFlag.Click);

            SavingStatus();
            await _deleteUseCase.Execute(Task);
            await SucessStatus();

            var navParameters = new NavigationParameters
                {
                    { "Refresh", 1 }
                };

            await _navigationService.GoBackAsync(navParameters);
        }

        public void Initialize(INavigationParameters parameters)
        {
            var task = parameters.GetValue<TaskModel>("Task");
            Task = new TaskModel
            {
                Id = task.Id,
                Description = task.Description,
                Category = task.Category,
                EndsAt = task.EndsAt,
                StartsAt = task.StartsAt,
                Percentage = task.Percentage,
                Title = task.Title
            };
            RaisePropertyChanged("Task");
            RaisePropertyChanged("TotalTime");
            RaisePropertyChanged("TimeStartsAt");
            RaisePropertyChanged("TimeEndsAt");
            RaisePropertyChanged("DateStartsAt");
            RaisePropertyChanged("DateEndsAt");
        }
    }
}
