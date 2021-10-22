using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.UserTask.Interfaces;

namespace Timerom.App.UseCase.UserTask.Local.GetAll
{
    public class GetAllUserTaskUseCase : IGetAllUserTaskUseCase
    {
        public async Task<TasksDetailsModel> Execute(DateTime date, IList<long> categoriesToFilterIds)
        {
            UserTaskDatabase database = await UserTaskDatabase.Instance();
            CategoryDatabase categoryDatabase = await CategoryDatabase.Instance();

            var models = await database.GetAll(date);

            var tasks = models.Select(c => Task.Run(async () =>
            {
                return new TaskModel
                {
                    Id = c.Id,
                    Description = c.Description,
                    Title = c.Title,
                    EndsAt = c.EndsAt,
                    StartsAt = c.StartsAt,
                    Category = await GetCategory(categoryDatabase, c.CategoryId)
                };
            })).ToList();

            await Task.WhenAll(tasks);

            var userTasks = tasks.Select(c => c.Result);

            return new TasksDetailsModel
            {
                Date = date,
                Tasks = new ObservableCollection<TaskModel>(FilterList(userTasks, categoriesToFilterIds)),
                Filters = new ObservableCollection<FilterModel>(userTasks.Select(c => c.Category.Parent.Id).Distinct().Select(c => new FilterModel
                {
                    Id = userTasks.First(w => w.Category.Parent.Id == c).Category.Parent.Id,
                    Name = userTasks.First(w => w.Category.Parent.Id == c).Category.Parent.Name,
                    Checked = categoriesToFilterIds.Any(k => k == userTasks.First(w => w.Category.Parent.Id == c).Category.Parent.Id)
                }).OrderBy(c => c.Name))
            };
        }

        private IList<TaskModel> FilterList(IEnumerable<TaskModel> userTasks, IList<long> categoriesToFilterIds)
        {
            var result = userTasks;

            if (categoriesToFilterIds.Any())
                result = userTasks.Where(c => categoriesToFilterIds.Any(k => c.Category.Parent.Id == k));

            return result.OrderBy(c => c.StartsAt).ThenBy(c => c.Title).ToList();
        }

        private async Task<Category> GetCategory(CategoryDatabase categoryDatabase, long categoryId)
        {
            var model = await categoryDatabase.GetById(categoryId);
            var parentCategory = await categoryDatabase.GetById(model.ParentCategoryId.Value);

            return new Category
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                Parent = new Category
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name
                }
            };
        }
    }
}
