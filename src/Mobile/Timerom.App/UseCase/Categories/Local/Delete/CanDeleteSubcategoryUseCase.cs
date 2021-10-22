using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Delete
{
    public class CanDeleteSubcategoryUseCase : ICanDeleteSubcategoryUseCase
    {
        public async Task Execute(Category category)
        {
            UserTaskDatabase database = await UserTaskDatabase.Instance();
            var exist = await database.ExistTaskForSubcategory(category.Id);

            if (exist)
                throw new ErrorOnValidationException(new List<string> { ResourceTextException.THERE_IS_TASK_ASSOCIATED_SUBCATEGORY });
        }
    }
}
