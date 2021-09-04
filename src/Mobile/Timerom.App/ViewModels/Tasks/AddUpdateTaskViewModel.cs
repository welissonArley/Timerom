using Prism.Navigation;
using System;
using Timerom.App.Model;

namespace Timerom.App.ViewModels.Tasks
{
    public class AddUpdateTaskViewModel : ViewModelBase, IInitialize
    {
        public TaskModel Task { get; set; }
        public string SubtractTime { get => Task == null ? "" : $"{(int)(Task.EndAt - Task.StartAt).TotalMinutes} min"; }

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
                EndAt = task.EndAt,
                StartAt = task.StartAt,
                Percentage = task.Percentage,
                Title = task.Title
            };
            RaisePropertyChanged("Task");
            RaisePropertyChanged("SubtractTime");
        }
    }
}
