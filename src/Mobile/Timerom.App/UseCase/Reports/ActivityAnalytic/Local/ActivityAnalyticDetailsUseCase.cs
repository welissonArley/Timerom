using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ValueObjects.Functions;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Local
{
    public class ActivityAnalyticDetailsUseCase : IActivityAnalyticDetailsUseCase
    {
        private readonly FuncCorrectDate _funcCorrectDate;

        private readonly Lazy<ICategoryReadOnlyRepository> repositoryReadOnly;
        private readonly Lazy<IUserTaskReadOnlyRepository> repositoryUserTask;

        public ActivityAnalyticDetailsUseCase(Lazy<ICategoryReadOnlyRepository> repositoryReadOnly,
            Lazy<IUserTaskReadOnlyRepository> repositoryUserTask)
        {
            this.repositoryReadOnly = repositoryReadOnly;
            this.repositoryUserTask = repositoryUserTask;

            _funcCorrectDate = new FuncCorrectDate();
        }

        public async Task<ActivityAnalyticStatisticsModel> Execute(DateTime date)
        {
            var activityAnalyticBase = new GetActivityAnalyticBase(repositoryReadOnly, repositoryUserTask);
            var userTasks = await activityAnalyticBase.GetUserTasks(date, date);

            var productiveTasks = userTasks.Where(c => c.Category.Type == CategoryType.Productive);
            var neutralTasks = userTasks.Where(c => c.Category.Type == CategoryType.Neutral);
            var unproductiveTasks = userTasks.Where(c => c.Category.Type == CategoryType.Unproductive);

            var tasksPerCategory = TasksPerCategory(userTasks, date);

            return new ActivityAnalyticStatisticsModel
            {
                Productive = new TaskAnalyticModel
                {
                    AmountOfTasks = productiveTasks.Count(),
                    Time = new TimeSpan(hours: 0, seconds: 0, minutes: TotalTime(productiveTasks, date))
                },
                Neutral = new TaskAnalyticModel
                {
                    AmountOfTasks = neutralTasks.Count(),
                    Time = new TimeSpan(hours: 0, seconds: 0, minutes: TotalTime(neutralTasks, date))
                },
                Unproductive = new TaskAnalyticModel
                {
                    AmountOfTasks = unproductiveTasks.Count(),
                    Time = new TimeSpan(hours: 0, seconds: 0, minutes: TotalTime(unproductiveTasks, date))
                },
                Activities = new ObservableCollection<ActivitiesAnalyticModel>(tasksPerCategory)
            };
        }

        private int TotalTime(IEnumerable<TaskModel> userTasks, DateTime searchDate)
        {
            return (int)userTasks.Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);
        }

        private List<ActivitiesAnalyticModel> TasksPerCategory(IEnumerable<TaskModel> tasks, DateTime searchDate)
        {
            var categoryIds = tasks.Select(c => c.Category.Parent.Id).Distinct();

            var totalTime = tasks.Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);

            var result = new List<ActivitiesAnalyticModel>(categoryIds.Count());

            foreach (var categoryId in categoryIds)
            {
                var tasksForTheCategory = tasks.Where(c => c.Category.Parent.Id == categoryId);

                var category = tasksForTheCategory.First().Category.Parent;

                var totalTimeCategory = tasksForTheCategory
                    .Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);

                result.Add(new ActivitiesAnalyticModel
                {
                    Name = category.Name,
                    Time = new TimeSpan(hours: 0, minutes: (int)totalTimeCategory, seconds: 0),
                    Percentage = Math.Round(100 * totalTimeCategory / totalTime, 2),
                    Type = category.Type,
                    Id = categoryId
                });
            }

            return result.OrderByDescending(c => c.Time).ThenBy(c => c.Name).ToList();
        }
    }
}
