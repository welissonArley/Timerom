using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ValueObjects.Functions;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Local
{
    public class ActivityAnalyticDetailsUseCase : IActivityAnalyticDetailsUseCase
    {
        private readonly FuncCorrectDate _funcCorrectDate;

        public ActivityAnalyticDetailsUseCase()
        {
            _funcCorrectDate = new FuncCorrectDate();
        }

        public async Task<ActivityAnalyticStatisticsModel> Execute(DateTime date)
        {
            var activityAnalyticBase = new GetActivityAnalyticBase();
            var userTasks = await activityAnalyticBase.GetUserTasks(date, date);

            var productiveTasks = userTasks.Where(c => c.Category.Type == CategoryType.Productive);
            var neutralTasks = userTasks.Where(c => c.Category.Type == CategoryType.Neutral);
            var unproductiveTasks = userTasks.Where(c => c.Category.Type == CategoryType.Unproductive);

            CategoryDatabase categoryDatabase = await CategoryDatabase.Instance();
            var categories = await categoryDatabase.GetAll();

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
                Activities = new ObservableCollection<ActivitiesAnalyticModel>(TasksPerCategory(categories, userTasks, date))
            };
        }

        private int TotalTime(IEnumerable<TaskModel> userTasks, DateTime searchDate)
        {
            return (int)userTasks.Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);
        }

        private List<ActivitiesAnalyticModel> TasksPerCategory(List<ValueObjects.Entity.Category> categories,
            IEnumerable<TaskModel> tasks, DateTime searchDate)
        {
            var result = new List<ActivitiesAnalyticModel>();

            var totalTime = tasks.Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);

            var subCategoryIds = tasks.Select(c => c.Category.Id).Distinct();

            var subCategoriesList = categories.Where(c => subCategoryIds.Any(k => k == c.Id));

            var categoryIds = subCategoriesList.Select(c => c.ParentCategoryId).Distinct();

            foreach (var categoryId in categoryIds)
            {
                var category = categories.First(c => c.Id == categoryId);
                var subCategories = subCategoriesList.Where(k => k.ParentCategoryId == categoryId);

                var totalTimeCategory = tasks.Where(c => subCategories.Any(k => k.Id == c.Category.Id))
                    .Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);

                result.Add(new ActivitiesAnalyticModel
                {
                    Name = category.Name,
                    Time = new TimeSpan(hours: 0, minutes: (int)totalTimeCategory, seconds: 0),
                    Percentage = Math.Round(100 * totalTimeCategory / totalTime, 2),
                    Type = category.Type,
                    Id = categoryId.Value
                });
            }

            return result.OrderByDescending(c => c.Time).ThenBy(c => c.Name).ToList();
        }
    }
}
