using Prism.Navigation;
using System;
using Timerom.App.Model;

namespace Timerom.App.ViewModels.Tasks
{
    public class AddUpdateTaskViewModel : ViewModelBase, IInitialize
    {
        public TaskModel Task { get; set; }
        public TimeSpan TotalTime => Task == null ? new TimeSpan() : (Task.EndsAt - Task.StartsAt);

        public TimeSpan StartsAt
        {
            get => Task == null ? new TimeSpan() : new TimeSpan(Task.StartsAt.Hour, Task.StartsAt.Minute, 0);
            set { Task.StartsAt = Task.StartsAt.Date + value; RaisePropertyChanged("TotalTime"); }
        }
        public TimeSpan EndsAt
        {
            get => Task == null ? new TimeSpan() : new TimeSpan(Task.EndsAt.Hour, Task.EndsAt.Minute, 0);
            set { Task.EndsAt = Task.EndsAt.Date + value; RaisePropertyChanged("TotalTime"); }
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
            RaisePropertyChanged("StartsAt");
            RaisePropertyChanged("EndsAt");
        }
    }
}
