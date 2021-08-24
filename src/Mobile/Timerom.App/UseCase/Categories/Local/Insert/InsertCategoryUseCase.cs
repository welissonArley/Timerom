using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Insert
{
    public class InsertCategoryUseCase : IInsertCategoryUseCase
    {
        public async Task Execute(Category category)
        {
            CategoryDatabase database = await CategoryDatabase.Instance();

            await Validate(category, database);

            await Save(category, database);
        }

        private async Task Save(Category category, CategoryDatabase database)
        {
            var parentCategoryId = await SaveParent(database, category);

            var tasks = category.Childrens.Select(c => Task.Run(() =>
            {
                _ = database.Save(new ValueObjects.Entity.Category
                {
                    Name = c.Name,
                    ParentCategoryId = parentCategoryId,
                    Type = category.Type
                });
            })).ToArray();

            Task.WaitAll(tasks);
        }
        private async Task<long> SaveParent(CategoryDatabase database, Category category)
        {
            var parentCategory = new ValueObjects.Entity.Category
            {
                Name = category.Name,
                Type = category.Type
            };

            await database.Save(parentCategory);

            return parentCategory.Id;
        }

        private async Task Validate(Category category, CategoryDatabase database)
        {
            var validation = await new InsertCategoryValidation(database).ValidateAsync(category);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
