using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Timerom.App.UseCase.Categories.Local.Delete
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        public async Task Execute(Category category)
        {
            CategoryDatabase database = await CategoryDatabase.Instance();

            ValueObjects.Entity.Category categoryModel = await database.GetById(category.Id);
            var childrensList = await database.GetChildrensByParentId(categoryModel.Id);

            var tasks = childrensList.Select(c => Task.Run(async () =>
            {
                await database.Delete(c);
            })).ToList();

            tasks.Add(Task.Run(async () => { await database.Delete(categoryModel); }));

            await Task.WhenAll(tasks);
        }
    }
}
