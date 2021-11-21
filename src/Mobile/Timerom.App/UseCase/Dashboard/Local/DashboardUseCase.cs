using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Dashboard.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ValueObjects.Functions;

namespace Timerom.App.UseCase.Dashboard.Local
{
    public class DashboardUseCase : IDashboardUseCase
    {
        private readonly FuncCorrectDate _funcCorrectDate;

        private readonly Lazy<IUserTaskReadOnlyRepository> repositoryUserTask;
        private readonly Lazy<ICategoryReadOnlyRepository> repository;
        private ICategoryReadOnlyRepository _repository => repository.Value;
        private IUserTaskReadOnlyRepository _repositoryUserTask => repositoryUserTask.Value;

        public DashboardUseCase(Lazy<ICategoryReadOnlyRepository> repository, Lazy<IUserTaskReadOnlyRepository> repositoryUserTask)
        {
            _funcCorrectDate = new FuncCorrectDate();
            this.repository = repository;
            this.repositoryUserTask = repositoryUserTask;
        }

        public async Task<DashboardModel> Execute(DateTime date)
        {
            var models = await _repositoryUserTask.GetAll(date);

            if (!models.Any())
                return new DashboardModel { TotalTasks = 0 };

            var categories = await _repository.GetAll();

            var totalProductive = CalculateTotalTimeMinutesPerCategory(categories, models, CategoryType.Productive, date);
            var totalNeutral = CalculateTotalTimeMinutesPerCategory(categories, models, CategoryType.Neutral, date);
            var totalUnproductive = CalculateTotalTimeMinutesPerCategory(categories, models, CategoryType.Unproductive, date);

            var sumMinutesProductiveNeutralUnproductive = totalProductive + totalNeutral + totalUnproductive;

            return new DashboardModel
            {
                TotalTasks = models.Count,
                ProductivePercentage = Convert.ToInt32(100 * totalProductive / sumMinutesProductiveNeutralUnproductive),
                NeutralPercentage = Convert.ToInt32(100 * totalNeutral / sumMinutesProductiveNeutralUnproductive),
                UnproductivePercentage = Convert.ToInt32(100 * totalUnproductive / sumMinutesProductiveNeutralUnproductive),
                Tasks = new ObservableCollection<DashboardTaskModel>(TasksPerCategory(categories, models, date))
            };
        }

        private double CalculateTotalTimeMinutesPerCategory(List<ValueObjects.Entity.Category> categories,
            List<ValueObjects.Entity.UserTask> tasks, CategoryType type, DateTime searchDate)
        {
            var list = categories.Where(c => c.Type == type && c.ParentCategoryId.HasValue);

            var taskList = tasks.Where(c => list.Any(k => k.Id == c.CategoryId));

            return taskList.Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);
        }

        private List<DashboardTaskModel> TasksPerCategory(List<ValueObjects.Entity.Category> categories,
            List<ValueObjects.Entity.UserTask> tasks, DateTime searchDate)
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

                var totalTime = tasks.Where(c => subCategories.Any(k => k.Id == c.CategoryId))
                    .Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);

                result.Add(new DashboardTaskModel
                {
                    Title = category.Name,
                    Hours = new TimeSpan(hours: 0, minutes: (int)totalTime, seconds: 0),
                    Percentage = funcPercentageOfDay.Execute(DateTime.Today.Date, DateTime.Today.Date.AddMinutes(totalTime)),
                    Category = category.Type,
                    CategoryId = categoryId.Value
                });
            }

            return result.OrderByDescending(c => c.Hours).ThenBy(c => c.Category).ThenBy(c => c.Title).ToList();
        }
    }
}
