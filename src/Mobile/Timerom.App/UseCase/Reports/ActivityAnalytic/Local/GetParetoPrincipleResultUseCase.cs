using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Dto;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Local
{
    public class GetParetoPrincipleResultUseCase : IGetParetoPrincipleResultUseCase
    {
        public async Task<ParetoPrincipleModel> Execute(ParetoPrincipleFilter filter)
        {
            var activityAnalyticBase = new GetActivityAnalyticBase();

            var userTasks = await activityAnalyticBase.GetUserTasks(filter.StartsAt, filter.EndsAt);

            if (!userTasks.Any())
                return new ParetoPrincipleModel();

            if (filter.CategoryId.HasValue)
                userTasks = userTasks.Where(c => c.Category.Parent.Id == filter.CategoryId);

            var totalTime = (int)userTasks.Sum(c => (c.EndsAt - c.StartsAt).TotalMinutes);

            var tasksPerCategory = await TasksPerCategory(totalTime, userTasks);

            ApplyParetoPrinciple(tasksPerCategory);

            return new ParetoPrincipleModel
            {
                PercentageResult = tasksPerCategory.Last(c => c.IsPartOf80Percent).AccumulatedPercentage,
                PercentageCause = Math.Round(100 * tasksPerCategory.Count(c => c.IsPartOf80Percent) / (double)tasksPerCategory.Count, 2),
                Ranking = new ObservableCollection<RankingParetoPrincipleModel>(tasksPerCategory)
            };
        }

        private async Task<List<RankingParetoPrincipleModel>> TasksPerCategory(int totalTimeForAllTasks, IEnumerable<TaskModel> tasks)
        {
            CategoryDatabase categoryDatabase = await CategoryDatabase.Instance();
            var categories = await categoryDatabase.GetAll();

            var categoryIds = tasks.Select(c => c.Category.Parent.Id).Distinct();

            var result = new List<RankingParetoPrincipleModel>(categoryIds.Count());

            for(var index = 0; index < categoryIds.Count(); index++)
            {
                var categoryId = categoryIds.ElementAt(index);

                result.Insert(index, CreateResultModel(categories, categoryId, tasks, totalTimeForAllTasks));
            }

            return result.OrderByDescending(c => c.Time).ToList();
        }

        private void ApplyParetoPrinciple(List<RankingParetoPrincipleModel> tasks)
        {
            var index = 1;
            double accumulatedPercentage = 0.0;
            bool haveFoundTheFirstTaskGet80Percent = false;

            foreach (var task in tasks)
            {
                accumulatedPercentage += task.Percentage;

                task.Index = index++;
                task.AccumulatedPercentage = accumulatedPercentage;

                if(!haveFoundTheFirstTaskGet80Percent && accumulatedPercentage >= 80.0)
                {
                    haveFoundTheFirstTaskGet80Percent = true;
                    task.IsPartOf80Percent = true;
                }
                else if (!haveFoundTheFirstTaskGet80Percent)
                    task.IsPartOf80Percent = true;
            }

            /*
             This is used just to make the sum of Percentage and the accumulated percentage equals to 100%
             */
            var lastTask = tasks.Last();
            lastTask.Percentage = Math.Round(lastTask.Percentage + 100.0 - accumulatedPercentage, 2);
            lastTask.AccumulatedPercentage = 100.0;
        }
    
        private RankingParetoPrincipleModel CreateResultModel(List<ValueObjects.Entity.Category> categories,
            long categoryId, IEnumerable<TaskModel> tasks, int totalTimeForAllTasks)
        {
            var category = categories.First(c => c.Id == categoryId);

            var tasksForTheCategory = tasks.Where(c => c.Category.Parent.Id == categoryId);

            var totalTimeCategory = (int)tasksForTheCategory.Sum(c => (c.EndsAt - c.StartsAt).TotalMinutes);

            return new RankingParetoPrincipleModel
            {
                AmountOfTasks = tasksForTheCategory.Count(),
                Time = new TimeSpan(hours: 0, seconds: 0, minutes: totalTimeCategory),
                Category = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Type = category.Type
                },
                Percentage = Math.Round(100 * totalTimeCategory / (double)totalTimeForAllTasks, 2)
            };
        }
    }
}
