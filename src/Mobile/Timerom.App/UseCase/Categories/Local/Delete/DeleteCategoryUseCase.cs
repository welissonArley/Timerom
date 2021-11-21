using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Delete
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        private readonly Lazy<ICategoryWriteOnlyRepository> repository;
        private readonly Lazy<ICategoryReadOnlyRepository> repositoryReadonly;
        private ICategoryWriteOnlyRepository _repository => repository.Value;
        private ICategoryReadOnlyRepository _repositoryReadonly => repositoryReadonly.Value;

        private readonly Lazy<IUserTaskReadOnlyRepository> repositoryTask;
        private IUserTaskReadOnlyRepository _repositoryTask => repositoryTask.Value;

        public DeleteCategoryUseCase(Lazy<ICategoryWriteOnlyRepository> repository, Lazy<ICategoryReadOnlyRepository> repositoryReadonly,
            Lazy<IUserTaskReadOnlyRepository> repositoryTask)
        {
            this.repository = repository;
            this.repositoryReadonly = repositoryReadonly;
            this.repositoryTask = repositoryTask;
        }

        public async Task Execute(Category category)
        {
            ValueObjects.Entity.Category categoryModel = await _repositoryReadonly.GetById(category.Id);
            var childrensList = await _repositoryReadonly.GetChildrensByParentId(categoryModel.Id);

            await Validate(childrensList);

            var tasks = childrensList.Select(c => Task.Run(async () =>
            {
                await _repository.Delete(c);
            })).ToList();

            tasks.Add(Task.Run(async () => { await _repository.Delete(categoryModel); }));

            await Task.WhenAll(tasks);
        }

        private async Task Validate(List<ValueObjects.Entity.Category> childrensList)
        {
            var tasks = childrensList.Select(c => Task.Run(async () =>
            {
                return await _repositoryTask.ExistTaskForSubcategory(c.Id);
            })).ToList();

            await Task.WhenAll(tasks);

            if(tasks.Any(c => c.Result))
                throw new ErrorOnValidationException(new List<string> { ResourceTextException.CATEGORY_CONTAIS_SUBCATEGORIES_ASSOCIATED_TASKS });
        }
    }
}
