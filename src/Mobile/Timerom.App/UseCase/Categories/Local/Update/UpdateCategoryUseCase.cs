using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Update
{
    public class UpdateCategoryUseCase : IUpdateCategoryUseCase
    {
        public async Task<Category> Execute(Category category)
        {
            Validate(category);

            return await Save(category);
        }

        private async Task<Category> Save(Category category)
        {
            CategoryDatabase database = await CategoryDatabase.Instance();

            ValueObjects.Entity.Category categoryModel = await database.GetById(category.Id);
            categoryModel.Name = category.Name;

            await database.Update(categoryModel);

            await RemoveCategoriesChildrens(database, category, categoryModel);
            await InsertNewCategoriesChildrens(database, category);

            var childrensList = await database.GetChildrensByParentId(categoryModel.Id);

            return new Category
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
                Type = categoryModel.Type,
                Childrens = new ObservableCollection<Category>(childrensList.Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type
                }).OrderBy(c => c.Name))
            };
        }

        private async Task InsertNewCategoriesChildrens(CategoryDatabase database, Category category)
        {
            var insertList = category.Childrens.Where(c => c.Id == 0).Select(c => new ValueObjects.Entity.Category
            {
                Name = c.Name,
                ParentCategoryId = category.Id,
                Type = category.Type
            }).ToList();

            await database.Save(insertList);
        }
        private async Task RemoveCategoriesChildrens(CategoryDatabase database, Category category, ValueObjects.Entity.Category categoryModel)
        {
            var childrensList = await database.GetChildrensByParentId(categoryModel.Id);

            var deletList = childrensList.Where(c => category.Childrens.All(k => k.Id != c.Id)).ToList();

            var tasks = deletList.Select(c => Task.Run(async() =>
            {
                await database.Delete(c);
            })).ToList();

            await Task.WhenAll(tasks);
        }

        private void Validate(Category category)
        {
            var validation = new UpdateCategoryValidation().Validate(category);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
