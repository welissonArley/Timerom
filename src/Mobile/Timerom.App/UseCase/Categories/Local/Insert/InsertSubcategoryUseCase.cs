using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Insert
{
    public class InsertSubcategoryUseCase : IInsertSubcategoryUseCase
    {
        private readonly Lazy<ICategoryReadOnlyRepository> repositoryReadOnly;
        private readonly Lazy<ICategoryWriteOnlyRepository> repository;
        private ICategoryWriteOnlyRepository _repository => repository.Value;

        public InsertSubcategoryUseCase(Lazy<ICategoryWriteOnlyRepository> repository, Lazy<ICategoryReadOnlyRepository> repositoryReadOnly)
        {
            this.repository = repository;
            this.repositoryReadOnly = repositoryReadOnly;
        }

        public async Task<Category> Execute(Category category, long parentId)
        {
            await Validate(category, parentId);

            return await Save(category, parentId);
        }

        private async Task<Category> Save(Category category, long parentId)
        {
            var model = new ValueObjects.Entity.Category
            {
                Name = category.Name,
                Type = category.Type,
                ParentCategoryId = parentId
            };

            await _repository.Save(model);

            return new Category
            {
                Name = category.Name,
                Type = category.Type,
                Id = model.Id
            };
        }

        private async Task Validate(Category category, long parentId)
        {
            var validation = await new InsertSubcategoryValidation(repositoryReadOnly.Value, parentId).ValidateAsync(category);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
