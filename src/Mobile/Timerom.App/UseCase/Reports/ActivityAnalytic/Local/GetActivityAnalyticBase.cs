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
            var model = await categoryDatabase.GetById(categoryId);

            return new Category
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type
            };
        }
    }
}
