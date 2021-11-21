using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Insert
{
    public class InsertCategoryUseCase : IInsertCategoryUseCase
    {
        private readonly Lazy<ICategoryReadOnlyRepository> repositoryReadOnly;
        private readonly Lazy<ICategoryWriteOnlyRepository> repository;
        private ICategoryWriteOnlyRepository _repository => repository.Value;

        public InsertCategoryUseCase(Lazy<ICategoryWriteOnlyRepository> repository, Lazy<ICategoryReadOnlyRepository> repositoryReadOnly)
        {
            this.repository = repository;
            this.repositoryReadOnly = repositoryReadOnly;
        }

        public async Task<Category> Execute(Category category)
        {
            await Validate(category);

            return await Save(category);
        }

        private async Task<Category> Save(Category category)
        {
            long parentCategoryId = await SaveParent(category);

            var childrensList = category.Childrens.Select(c => new ValueObjects.Entity.Category
            {
                Name = c.Name,
                ParentCategoryId = parentCategoryId,
                Type = category.Type
            }).ToList();

            await _repository.Save(childrensList);

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
        private async Task<long> SaveParent(Category category)
        {
            var parentCategory = new ValueObjects.Entity.Category
            {
                Name = category.Name,
                Type = category.Type
            };

            await _repository.Save(parentCategory);

            return parentCategory.Id;
        }

        private async Task Validate(Category category)
        {
            var validation = await new InsertCategoryValidation(repositoryReadOnly.Value).ValidateAsync(category);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
