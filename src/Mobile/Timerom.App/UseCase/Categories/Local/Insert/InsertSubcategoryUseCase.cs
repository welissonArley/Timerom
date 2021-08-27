using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Insert
{
    public class InsertSubcategoryUseCase : IInsertSubcategoryUseCase
    {
        public async Task<Category> Execute(Category category, long parentId)
        {
            CategoryDatabase database = await CategoryDatabase.Instance();

            await Validate(category, parentId, database);

            return await Save(category, parentId, database);
        }

        private async Task<Category> Save(Category category, long parentId, CategoryDatabase database)
        {
            var model = new ValueObjects.Entity.Category
            {
                Name = category.Name,
                Type = category.Type,
                ParentCategoryId = parentId
            };

            await database.Save(model);

            return new Category
            {
                Name = category.Name,
                Type = category.Type,
                Id = model.Id
            };
        }

        private async Task Validate(Category category, long parentId, CategoryDatabase database)
        {
            var validation = await new InsertSubcategoryValidation(database, parentId).ValidateAsync(category);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
