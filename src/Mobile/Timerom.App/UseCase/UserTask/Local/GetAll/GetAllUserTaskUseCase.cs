using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.UserTask.Interfaces;

namespace Timerom.App.UseCase.UserTask.Local.GetAll
{
    public class GetAllUserTaskUseCase : IGetAllUserTaskUseCase
    {
        private readonly Lazy<IUserTaskReadOnlyRepository> repositoryUserTask;
        private readonly Lazy<ICategoryReadOnlyRepository> repositoryReadOnly;
        private ICategoryReadOnlyRepository _repositoryReadOnly => repositoryReadOnly.Value;
        private IUserTaskReadOnlyRepository _repositoryUserTask => repositoryUserTask.Value;

        public GetAllUserTaskUseCase(Lazy<ICategoryReadOnlyRepository> repositoryReadOnly,
            Lazy<IUserTaskReadOnlyRepository> repositoryUserTask)
        {
            this.repositoryReadOnly = repositoryReadOnly;
            this.repositoryUserTask = repositoryUserTask;
        }

        public async Task<TasksDetailsModel> Execute(DateTime date, IList<long> categoriesToFilterIds)
        {
            var models = await _repositoryUserTask.GetAll(date);

            var tasks = models.Select(c => Task.Run(async () =>
            {
                return new TaskModel
                {
                    Id = c.Id,
                    Description = c.Description,
                    Title = c.Title,
                    EndsAt = c.EndsAt,
                    StartsAt = c.StartsAt,
                    Category = await GetCategory(c.CategoryId)
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

        private async Task<Category> GetCategory(long categoryId)
        {
            var model = await _repositoryReadOnly.GetById(categoryId);
            var parentCategory = await _repositoryReadOnly.GetById(model.ParentCategoryId.Value);

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
