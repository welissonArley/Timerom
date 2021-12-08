using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.Categories.Local.Delete
{
    public class DeleteSubcategoryUseCase : IDeleteSubcategoryUseCase
    {
        private readonly Lazy<IUserTaskReadOnlyRepository> repositoryUserTask;
        private readonly Lazy<ICategoryWriteOnlyRepository> repository;
        private readonly Lazy<ICategoryReadOnlyRepository> repositoryReadonly;
        private ICategoryWriteOnlyRepository _repository => repository.Value;
        private ICategoryReadOnlyRepository _repositoryReadonly => repositoryReadonly.Value;
        private IUserTaskReadOnlyRepository _repositoryUserTask => repositoryUserTask.Value;

        public DeleteSubcategoryUseCase(Lazy<ICategoryWriteOnlyRepository> repository, Lazy<ICategoryReadOnlyRepository> repositoryReadonly,
            Lazy<IUserTaskReadOnlyRepository> repositoryUserTask)
        {
            this.repository = repository;
            this.repositoryReadonly = repositoryReadonly;
            this.repositoryUserTask = repositoryUserTask;
        }

        public async Task Execute(Category category)
        {
            await Validate(category.Id);

            ValueObjects.Entity.Category categoryModel = await _repositoryReadonly.GetById(category.Id);

            await _repository.Delete(categoryModel);
        }

        private async Task Validate(long id)
        {
            var exist = await _repositoryUserTask.ExistTaskForSubcategory(id);

            if (exist)
                throw new ErrorOnValidationException(new List<string> { ResourceTextException.THERE_IS_TASK_ASSOCIATED_SUBCATEGORY });
        }
    }
}
