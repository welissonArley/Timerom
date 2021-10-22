using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Delete
{
    public class DeleteSubcategoryUseCase : IDeleteSubcategoryUseCase
    {
        public async Task Execute(Category category)
        {
            await Validate(category.Id);

            CategoryDatabase database = await CategoryDatabase.Instance();

            ValueObjects.Entity.Category categoryModel = await database.GetById(category.Id);

            await database.Delete(categoryModel);
        }

        private async Task Validate(long id)
        {
            UserTaskDatabase database = await UserTaskDatabase.Instance();
            var exist = await database.ExistTaskForSubcategory(id);

            if (exist)
                throw new ErrorOnValidationException(new List<string> { ResourceTextException.THERE_IS_TASK_ASSOCIATED_SUBCATEGORY });
        }
    }
}
