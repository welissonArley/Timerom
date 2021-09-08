using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.App.ValueObjects.Functions;

namespace Timerom.App.UseCase.UserTask.Local.GetAll
{
    public class GetAllUserTaskUseCase : IGetAllUserTaskUseCase
    {
        public async Task<IList<TaskModel>> Execute(DateTime date)
        {
            UserTaskDatabase database = await UserTaskDatabase.Instance();
            CategoryDatabase categoryDatabase = await CategoryDatabase.Instance();

            var models = await database.GetAll(date);

            var percentageFunction = new FuncPercentageOfDay();

            var tasks = models.Select(c => Task.Run(async () =>
            {
                return new TaskModel
                {
                    Id = c.Id,
                    Description = c.Description,
                    Title = c.Title,
                    EndsAt = c.EndsAt,
                    StartsAt = c.StartsAt,
                    Percentage = percentageFunction.Execute(c.StartsAt, c.EndsAt),
                    Category = await GetCategory(categoryDatabase, c.CategoryId)
                };
            })).ToList();

            await Task.WhenAll(tasks);

            return tasks.Select(c => c.Result).OrderBy(c => c.StartsAt).ThenBy(c => c.Title).ToList();
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
