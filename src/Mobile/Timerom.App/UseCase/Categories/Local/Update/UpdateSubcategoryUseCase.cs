using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Update
{
    public class UpdateSubcategoryUseCase : IUpdateSubcategoryUseCase
    {
        private readonly Lazy<ICategoryReadOnlyRepository> repositoryReadOnly;
        private readonly Lazy<ICategoryWriteOnlyRepository> repositoryWriteOnly;
        private ICategoryWriteOnlyRepository _repositoryWriteOnly => repositoryWriteOnly.Value;
        private ICategoryReadOnlyRepository _repositoryReadOnly => repositoryReadOnly.Value;

        public UpdateSubcategoryUseCase(Lazy<ICategoryWriteOnlyRepository> repositoryWriteOnly, Lazy<ICategoryReadOnlyRepository> repositoryReadOnly)
        {
            this.repositoryWriteOnly = repositoryWriteOnly;
            this.repositoryReadOnly = repositoryReadOnly;
        }

        public async Task Execute(Category category, long parentId)
        {
            await Validate(category, parentId);

            await Save(category);
        }

        private async Task Save(Category category)
        {
            ValueObjects.Entity.Category categoryModel = await _repositoryReadOnly.GetById(category.Id);
            categoryModel.Name = category.Name;

            await _repositoryWriteOnly.Update(categoryModel);
        }

        private async Task Validate(Category category, long parentId)
        {
            var validation = await new UpdateSubcategoryValidation(_repositoryReadOnly, parentId).ValidateAsync(category);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
