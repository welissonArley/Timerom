using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Local
{
    public class GetActivityAnalyticBase
    {
        private readonly Lazy<IUserTaskReadOnlyRepository> repositoryUserTask;
        private readonly Lazy<ICategoryReadOnlyRepository> repositoryReadOnly;
        private ICategoryReadOnlyRepository _repositoryReadOnly => repositoryReadOnly.Value;
        private IUserTaskReadOnlyRepository _repositoryUserTask => repositoryUserTask.Value;

        public GetActivityAnalyticBase(Lazy<ICategoryReadOnlyRepository> repositoryReadOnly,
            Lazy<IUserTaskReadOnlyRepository> repositoryUserTask)
        {
            this.repositoryReadOnly = repositoryReadOnly;
            this.repositoryUserTask = repositoryUserTask;
        }

        public async Task<System.Collections.Generic.IEnumerable<TaskModel>> GetUserTasks(DateTime date1, DateTime date2)
        {
            var models = await _repositoryUserTask.GetBetweenDates(date1, date2);

            var tasks = models.Select(c => Task.Run(async () =>
            {
                return new TaskModel
                {
                    Title = c.Title,
                    EndsAt = c.EndsAt,
                    StartsAt = c.StartsAt,
                    Category = await GetCategory(c.CategoryId)
                };
            })).ToList();

            await Task.WhenAll(tasks);

            return tasks.Select(c => c.Result);
        }

        private async Task<Category> GetCategory(long categoryId)
        {
            var subcategoryModel = await _repositoryReadOnly.GetById(categoryId);
            var categoryModel = await _repositoryReadOnly.GetById(subcategoryModel.ParentCategoryId.Value);

            return new Category
            {
                Id = subcategoryModel.Id,
                Name = subcategoryModel.Name,
                Type = subcategoryModel.Type,
                Parent = new Category
                {
                    Id = categoryModel.Id,
                    Name = categoryModel.Name,
                    Type = categoryModel.Type
                }
            };
        }
    }
}
