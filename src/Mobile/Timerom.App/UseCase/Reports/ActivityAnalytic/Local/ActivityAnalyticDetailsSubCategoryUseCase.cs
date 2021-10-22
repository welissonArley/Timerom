using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Functions;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Local
{
    public class ActivityAnalyticDetailsSubCategoryUseCase : IActivityAnalyticDetailsSubCategoryUseCase
    {
        private readonly FuncCorrectDate _funcCorrectDate;

        public ActivityAnalyticDetailsSubCategoryUseCase()
        {
            _funcCorrectDate = new FuncCorrectDate();
        }

        public async Task<ObservableCollection<ActivitiesAnalyticModel>> Execute(long subcategoryId, DateTime date)
        {
            var activityAnalyticBase = new GetActivityAnalyticBase();
            var userTasks = await activityAnalyticBase.GetUserTasks(date, date);

            userTasks = userTasks.Where(c => c.Category.Parent.Id == subcategoryId);

            return new ObservableCollection<ActivitiesAnalyticModel>(TasksPerSubcategory(userTasks, date));
        }

        private List<ActivitiesAnalyticModel> TasksPerSubcategory(IEnumerable<TaskModel> tasks, DateTime searchDate)
        {
            var totalTime = tasks.Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);

            var subCategoryIds = tasks.Select(c => c.Category.Id).Distinct();

            var result = new List<ActivitiesAnalyticModel>(subCategoryIds.Count());

            foreach (var subcategoryId in subCategoryIds)
            {
                var tasksForTheSubcategory = tasks.Where(c => c.Category.Id == subcategoryId);

                var subcategory = tasksForTheSubcategory.First().Category;

                var totalTimeCategory = tasksForTheSubcategory
                    .Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);

                result.Add(new ActivitiesAnalyticModel
                {
                    Name = subcategory.Name,
                    Time = new TimeSpan(hours: 0, minutes: (int)totalTimeCategory, seconds: 0),
                    Percentage = Math.Round(100 * totalTimeCategory / totalTime, 2),
                    Type = subcategory.Type
                });
            }

            return result.OrderByDescending(c => c.Time).ThenBy(c => c.Name).ToList();
        }
    }
}
