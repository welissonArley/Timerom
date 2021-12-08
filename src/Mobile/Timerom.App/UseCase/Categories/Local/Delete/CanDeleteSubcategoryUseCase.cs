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
    public class CanDeleteSubcategoryUseCase : ICanDeleteSubcategoryUseCase
    {
        private readonly Lazy<IUserTaskReadOnlyRepository> repository;
        private IUserTaskReadOnlyRepository _repository => repository.Value;

        public CanDeleteSubcategoryUseCase(Lazy<IUserTaskReadOnlyRepository> repository)
        {
            this.repository = repository;
        }

        public async Task Execute(Category category)
        {
            var exist = await _repository.ExistTaskForSubcategory(category.Id);

            if (exist)
                throw new ErrorOnValidationException(new List<string> { ResourceTextException.THERE_IS_TASK_ASSOCIATED_SUBCATEGORY });
        }
    }
}
