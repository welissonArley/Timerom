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
            if (filter.CategoryId.HasValue)
                userTasks = userTasks.Where(c => c.Category.Parent.Id == filter.CategoryId);

            var totalTime = (int)userTasks.Sum(c => (c.EndsAt - c.StartsAt).TotalMinutes);

            var tasksPerCategory = await TasksPerCategory(totalTime, userTasks);

            ApplyParetoPrinciple(totalTime, tasksPerCategory);

            return new ParetoPrincipleModel
            {
                PercentageResult = tasksPerCategory.Last(c => c.IsPartOf80Percent).AccumulatedPercentage,
                PercentageCause = Math.Round(100 * tasksPerCategory.Count(c => c.IsPartOf80Percent) / (double)tasksPerCategory.Count, 2),
                Ranking = new ObservableCollection<RankingParetoPrincipleModel>(tasksPerCategory)
            };
        }

        private async Task<List<RankingParetoPrincipleModel>> TasksPerCategory(int totalTimeForAllTasks, IEnumerable<TaskModel> tasks)
        {
            var result = new List<RankingParetoPrincipleModel>();

            CategoryDatabase categoryDatabase = await CategoryDatabase.Instance();
            var categories = await categoryDatabase.GetAll();

            var subCategoryIds = tasks.Select(c => c.Category.Id).Distinct();

            var subCategoriesList = categories.Where(c => subCategoryIds.Any(k => k == c.Id));

            var categoryIds = subCategoriesList.Select(c => c.ParentCategoryId).Distinct();

            foreach (var categoryId in categoryIds)
            {
                var category = categories.First(c => c.Id == categoryId);
                var subCategories = subCategoriesList.Where(k => k.ParentCategoryId == categoryId);

                var tasksForTheCategory = tasks.Where(c => subCategories.Any(k => k.Id == c.Category.Id));
                var totalTimeCategory = (int)tasksForTheCategory.Sum(c => (c.EndsAt - c.StartsAt).TotalMinutes);

                result.Add(new RankingParetoPrincipleModel
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
                });
            }

            return result.OrderByDescending(c => c.Time).ToList();
        }

        private void ApplyParetoPrinciple(int totalTimeForAllTasks, List<RankingParetoPrincipleModel> tasks)
        {
            var index = 1;
            double accumulatedPercentage = 0.0;

            foreach (var task in tasks)
            {
                accumulatedPercentage += task.Percentage;

                task.Index = index++;
                task.AccumulatedPercentage = accumulatedPercentage;
            }

            var firstTaskGet80Percent = tasks.First(c => c.Percentage >= 80.0);
            for(index = 0; index <= tasks.IndexOf(firstTaskGet80Percent); index++)
            {
                var task = tasks.ElementAt(index);
                task.IsPartOf80Percent = true;
            }
        }
    }
}
