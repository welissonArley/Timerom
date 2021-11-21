using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Update
{
    public class UpdateCategoryUseCase : IUpdateCategoryUseCase
    {
        private readonly Lazy<IUserTaskReadOnlyRepository> repositoryUserTask;
        private readonly Lazy<ICategoryReadOnlyRepository> repositoryReadOnly;
        private readonly Lazy<ICategoryWriteOnlyRepository> repositoryWriteOnly;
        private ICategoryWriteOnlyRepository _repositoryWriteOnly => repositoryWriteOnly.Value;
        private ICategoryReadOnlyRepository _repositoryReadOnly => repositoryReadOnly.Value;
        private IUserTaskReadOnlyRepository _repositoryUserTask => repositoryUserTask.Value;

        public UpdateCategoryUseCase(Lazy<ICategoryWriteOnlyRepository> repositoryWriteOnly,
            Lazy<ICategoryReadOnlyRepository> repositoryReadOnly, Lazy<IUserTaskReadOnlyRepository> repositoryUserTask)
        {
            this.repositoryWriteOnly = repositoryWriteOnly;
            this.repositoryReadOnly = repositoryReadOnly;
            this.repositoryUserTask = repositoryUserTask;
        }

        public async Task<Category> Execute(Category category)
        {
            await Validate(category);

            return await Save(category);
        }

        private async Task<Category> Save(Category category)
        {
            ValueObjects.Entity.Category categoryModel = await _repositoryReadOnly.GetById(category.Id);
            
            await RemoveCategoriesChildrens(category, categoryModel);
            await InsertNewCategoriesChildrens(category);

            categoryModel.Name = category.Name;
            await _repositoryWriteOnly.Update(categoryModel);

            var childrensList = await _repositoryReadOnly.GetChildrensByParentId(categoryModel.Id);

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

        private async Task InsertNewCategoriesChildrens(Category category)
        {
            var insertList = category.Childrens.Where(c => c.Id == 0).Select(c => new ValueObjects.Entity.Category
            {
                Name = c.Name,
                ParentCategoryId = category.Id,
                Type = category.Type
            }).ToList();

            await _repositoryWriteOnly.Save(insertList);
        }
        private async Task RemoveCategoriesChildrens(Category category, ValueObjects.Entity.Category categoryModel)
        {
            var childrensList = await _repositoryReadOnly.GetChildrensByParentId(categoryModel.Id);

            var deletList = childrensList.Where(c => category.Childrens.All(k => k.Id != c.Id)).ToList();

            await ValidateIfCanDeleteSubcategories(deletList);

            var tasks = deletList.Select(c => Task.Run(async() =>
            {
                await _repositoryWriteOnly.Delete(c);
            })).ToList();

            await Task.WhenAll(tasks);
        }

        private async Task Validate(Category category)
        {
            var validation = await new UpdateCategoryValidation(_repositoryReadOnly).ValidateAsync(category);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }

        private async Task ValidateIfCanDeleteSubcategories(List<ValueObjects.Entity.Category> deletList)
        {
            var tasks = deletList.Select(c => Task.Run(async () =>
            {
                return await _repositoryUserTask.ExistTaskForSubcategory(c.Id);
            })).ToList();

            await Task.WhenAll(tasks);

            if (tasks.Any(c => c.Result))
                throw new ErrorOnValidationException(new List<string> { ResourceTextException.SOME_SUBCATEGORIES_CANNOT_REMOVED_ASSOCIATED_TASKS });
        }
    }
}
