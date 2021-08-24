using System.Collections.ObjectModel;
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
        public async Task<Category> Execute(Category category)
        {
            CategoryDatabase database = await CategoryDatabase.Instance();

            await Validate(category, database);

            return await Save(category, database);
        }

        private async Task<Category> Save(Category category, CategoryDatabase database)
        {
            long parentCategoryId = await SaveParent(database, category);

            var childrensList = category.Childrens.Select(c => new ValueObjects.Entity.Category
            {
                Name = c.Name,
                ParentCategoryId = parentCategoryId,
                Type = category.Type
            }).ToList();

            await database.Save(childrensList);

            return new Category
            {
                Id = parentCategoryId,
                Name = category.Name,
                Type = category.Type,
                Childrens = new ObservableCollection<Category>(childrensList.Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type
                }).OrderBy(c => c.Name))
            };
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
