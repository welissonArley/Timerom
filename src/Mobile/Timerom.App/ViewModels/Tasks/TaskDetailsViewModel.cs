﻿using Prism.Navigation;
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
                        StartAt = DateTime.Today.Date.AddHours(1),
                        EndAt = DateTime.Today.Date.AddHours(2),
                        Percentage = 3,
                        Type = ValueObjects.Enuns.CategoryType.Neutral
                    },
                    new TaskModel
                    {
                        Id = 1,
                        Title = "Youtube",
                        StartAt = DateTime.Today.Date.AddHours(2),
                        EndAt = DateTime.Today.Date.AddHours(3),
                        Percentage = 3.5,
                        Type = ValueObjects.Enuns.CategoryType.Unproductive
                    },
                    new TaskModel
                    {
                        Id = 1,
                        Title = "Reading book",
                        StartAt = DateTime.Today.Date.AddHours(1),
                        EndAt = DateTime.Today.Date.AddHours(2),
                        Percentage = 37,
                        Type = ValueObjects.Enuns.CategoryType.Productive
                    }
                }
            };

            RaisePropertyChanged("Model");
        }
    }
}
