using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.UserTask.Interfaces;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Tasks
{
    public class AddUpdateTaskViewModel : ViewModelBase, IInitialize
    {
        private readonly Lazy<IInsertTaskUseCase> insertTaskUseCase;
        private IInsertTaskUseCase _insertTaskUseCase => insertTaskUseCase.Value;

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

        public AddUpdateTaskViewModel(Lazy<IInsertTaskUseCase> insertTaskUseCase, Lazy<INavigationService> navigationService) : base(navigationService)
        {
            this.insertTaskUseCase = insertTaskUseCase;
            SaveCommand = new AsyncCommand(SaveCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
        }

        private async Task SaveCommandExecuted()
        {
            SavingStatus();

            if (Task.Id == 0)
                await CreateTask();
        }

        private async Task CreateTask()
        {
            await _insertTaskUseCase.Execute(Task);

            await SucessStatus();

            await _navigationService.GoBackToRootAsync();
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
