using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Local
{
    public class GetActivityAnalyticBase
    {
        public async Task<System.Collections.Generic.IEnumerable<TaskModel>> GetUserTasks(DateTime date1, DateTime date2)
        {
            UserTaskDatabase database = await UserTaskDatabase.Instance();
            CategoryDatabase categoryDatabase = await CategoryDatabase.Instance();

            var models = await database.GetBetweenDates(date1, date2);

            var tasks = models.Select(c => Task.Run(async () =>
            {
                return new TaskModel
                {
                    Title = c.Title,
                    EndsAt = c.EndsAt,
                    StartsAt = c.StartsAt,
                    Category = await GetCategory(categoryDatabase, c.CategoryId)
                };
            })).ToList();

            await Task.WhenAll(tasks);

            return tasks.Select(c => c.Result);
        }

        private async Task<Category> GetCategory(CategoryDatabase categoryDatabase, long categoryId)
        {
            var subcategoryModel = await categoryDatabase.GetById(categoryId);
            var categoryModel = await categoryDatabase.GetById(subcategoryModel.ParentCategoryId.Value);

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
