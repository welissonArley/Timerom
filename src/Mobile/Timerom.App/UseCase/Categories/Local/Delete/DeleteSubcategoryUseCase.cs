using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Timerom.App.UseCase.Categories.Local.Delete
{
    public class DeleteSubcategoryUseCase : IDeleteSubcategoryUseCase
    {
        public async Task Execute(Category category)
        {
            CategoryDatabase database = await CategoryDatabase.Instance();

            ValueObjects.Entity.Category categoryModel = await database.GetById(category.Id);

            await database.Delete(categoryModel);
        }
    }
}
