using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Delete
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        public async Task Execute(Category category)
        {
            CategoryDatabase database = await CategoryDatabase.Instance();

            ValueObjects.Entity.Category categoryModel = await database.GetById(category.Id);
            var childrensList = await database.GetChildrensByParentId(categoryModel.Id);

            await Validate(childrensList);

            var tasks = childrensList.Select(c => Task.Run(async () =>
            {
                await database.Delete(c);
            })).ToList();

            tasks.Add(Task.Run(async () => { await database.Delete(categoryModel); }));

            await Task.WhenAll(tasks);
        }

        private async Task Validate(List<ValueObjects.Entity.Category> childrensList)
        {
            UserTaskDatabase database = await UserTaskDatabase.Instance();

            var tasks = childrensList.Select(c => Task.Run(async () =>
            {
                return await database.ExistTaskForSubcategory(c.Id);
            })).ToList();

            await Task.WhenAll(tasks);

            if(tasks.Any(c => c.Result))
                throw new ErrorOnValidationException(new List<string> { ResourceTextException.CATEGORY_CONTAIS_SUBCATEGORIES_ASSOCIATED_TASKS });
        }
    }
}
