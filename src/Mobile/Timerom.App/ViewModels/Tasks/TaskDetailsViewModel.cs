using Prism.Navigation;
using System;
using Timerom.App.Model;

namespace Timerom.App.ViewModels.Tasks
{
    public class TaskDetailsViewModel : ViewModelBase, IInitialize
    {
        public TasksDetailsModel Model { get; set; }

        public TaskDetailsViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
        }

        public void Initialize(INavigationParameters parameters)
        {
            Model = new TasksDetailsModel
            {
                Date = DateTime.Today,
                Tasks = new System.Collections.ObjectModel.ObservableCollection<TaskModel>
                {
                    new TaskModel
                    {
                        Id = 1,
                        Title = "Breakfast",
                        StartsAt = DateTime.Today.Date.AddHours(1),
                        EndsAt = DateTime.Today.Date.AddHours(2),
                        Percentage = 3,
                        Category = new Model.Category { Type = ValueObjects.Enuns.CategoryType.Neutral}
                    },
                    new TaskModel
                    {
                        Id = 1,
                        Title = "Youtube",
                        StartsAt = DateTime.Today.Date.AddHours(2),
                        EndsAt = DateTime.Today.Date.AddHours(3),
                        Percentage = 3.5,
                        Category = new Model.Category { Type = ValueObjects.Enuns.CategoryType.Unproductive}
                    },
                    new TaskModel
                    {
                        Id = 1,
                        Title = "Reading book",
                        StartsAt = DateTime.Today.Date.AddHours(1),
                        EndsAt = DateTime.Today.Date.AddHours(2),
                        Percentage = 37,
                        Category = new Model.Category { Type = ValueObjects.Enuns.CategoryType.Productive}
                    }
                }
            };

            RaisePropertyChanged("Model");
        }
    }
}
