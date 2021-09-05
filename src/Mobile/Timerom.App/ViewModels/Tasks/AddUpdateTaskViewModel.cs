using Prism.Navigation;
using System;
using Timerom.App.Model;

namespace Timerom.App.ViewModels.Tasks
{
    public class AddUpdateTaskViewModel : ViewModelBase, IInitialize
    {
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
            set { Task.StartsAt = value.Date + TimeStartsAt; RaisePropertyChanged("TotalTime"); RaisePropertyChanged("DateStartsAt"); RaisePropertyChanged("DateEndsAt"); }
        }
        public DateTime DateEndsAt
        {
            get => Task == null ? DateTime.Now : new DateTime(Task.EndsAt.Year, Task.EndsAt.Month, Task.EndsAt.Day);
            set { Task.EndsAt = value.Date + TimeEndsAt; RaisePropertyChanged("TotalTime"); RaisePropertyChanged("DateStartsAt"); RaisePropertyChanged("DateEndsAt"); }
        }

        public AddUpdateTaskViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
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
