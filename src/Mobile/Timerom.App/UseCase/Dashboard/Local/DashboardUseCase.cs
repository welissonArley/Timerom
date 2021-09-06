using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Dashboard.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ValueObjects.Functions;

namespace Timerom.App.UseCase.Dashboard.Local
{
    public class DashboardUseCase : IDashboardUseCase
    {
        public async Task<DashboardModel> Execute(DateTime date)
        {
            UserTaskDatabase database = await UserTaskDatabase.Instance();
            
            var models = await database.GetAll(date);

            if (!models.Any())
                return null;

            CategoryDatabase categoryDatabase = await CategoryDatabase.Instance();
            var categories = await categoryDatabase.GetAll();

            var totalProductive = CalculateTotalTimeMinutesPerCategory(categories, models, CategoryType.Productive);
            var totalNeutral = CalculateTotalTimeMinutesPerCategory(categories, models, CategoryType.Neutral);
            var totalUnproductive = CalculateTotalTimeMinutesPerCategory(categories, models, CategoryType.Unproductive);

            var sumMinutesProductiveNeutralUnproductive = totalProductive + totalNeutral + totalUnproductive;

            return new DashboardModel
            {
                TotalTasks = models.Count,
                ProductivePercentage = Convert.ToInt32(100 * totalProductive / sumMinutesProductiveNeutralUnproductive),
                NeutralPercentage = Convert.ToInt32(100 * totalNeutral / sumMinutesProductiveNeutralUnproductive),
                UnproductivePercentage = Convert.ToInt32(100 * totalUnproductive / sumMinutesProductiveNeutralUnproductive),
                Tasks = new ObservableCollection<DashboardTaskModel>(TasksPerCategory(categories, models))
            };
        }

        private double CalculateTotalTimeMinutesPerCategory(List<ValueObjects.Entity.Category> categories,
            List<ValueObjects.Entity.UserTask> tasks, CategoryType type)
        {
            var list = categories.Where(c => c.Type == type && c.ParentCategoryId.HasValue);

            var taskList = tasks.Where(c => list.Any(k => k.Id == c.CategoryId));

            return taskList.Sum(c => (c.EndsAt - c.StartsAt).TotalMinutes);
        }

        private List<DashboardTaskModel> TasksPerCategory(List<ValueObjects.Entity.Category> categories,
            List<ValueObjects.Entity.UserTask> tasks)
        {
            var funcPercentageOfDay = new FuncPercentageOfDay();

            var result = new List<DashboardTaskModel>();

            var subCategoryIds = tasks.Select(c => c.CategoryId).Distinct();

            var subCategoriesList = categories.Where(c => subCategoryIds.Any(k => k == c.Id));

            var categoryIds = subCategoriesList.Select(c => c.ParentCategoryId).Distinct();

            foreach(var categoryId in categoryIds)
            {
                var category = categories.First(c => c.Id == categoryId);
                var subCategories = subCategoriesList.Where(k => k.ParentCategoryId == categoryId);

                var totalTime = Math.Round(tasks.Where(c => subCategories.Any(k => k.Id == c.CategoryId))
                    .Sum(c => (c.EndsAt - c.StartsAt).TotalMinutes), 2);

                result.Add(new DashboardTaskModel
                {
                    Title = category.Name,
                    Hours = Math.Round(totalTime/60.0, 2),
                    Percentage = funcPercentageOfDay.Execute(DateTime.Today.Date, DateTime.Today.Date.AddMinutes(totalTime)),
                    Category = category.Type
                });
            }

            return result.OrderByDescending(c => c.Hours).ThenBy(c => c.Category).ThenBy(c => c.Title).ToList();
        }
    }
}
