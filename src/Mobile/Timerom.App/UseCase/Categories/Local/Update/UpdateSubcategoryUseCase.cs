using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Update
{
    public class UpdateSubcategoryUseCase : IUpdateSubcategoryUseCase
    {
        public async Task Execute(Category category, long parentId)
        {
            CategoryDatabase database = await CategoryDatabase.Instance();

            await Validate(category, parentId, database);

            await Save(category, database);
        }

        private async Task Save(Category category, CategoryDatabase database)
        {
            ValueObjects.Entity.Category categoryModel = await database.GetById(category.Id);
            categoryModel.Name = category.Name;

            await database.Update(categoryModel);
        }

        private async Task Validate(Category category, long parentId, CategoryDatabase database)
        {
            var validation = await new UpdateSubcategoryValidation(database, parentId).ValidateAsync(category);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
