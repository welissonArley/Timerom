using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.UserTask.Local.Insert
{
    public class InsertTaskUseCase : IInsertTaskUseCase
    {
        private readonly Lazy<IUserTaskWriteOnlyRepository> repository;
        private IUserTaskWriteOnlyRepository _repositoryUserTask => repository.Value;

        public InsertTaskUseCase(Lazy<IUserTaskWriteOnlyRepository> repository)
        {
            this.repository = repository;
        }

        public async Task Execute(TaskModel task)
        {
            Validate(task);

            await Save(task);
        }

        private async Task Save(TaskModel task)
        {
            var model = new ValueObjects.Entity.UserTask
            {
                Title = task.Title,
                Description = task.Description,
                StartsAt = task.StartsAt,
                EndsAt = task.EndsAt,
                CategoryId = task.Category.Id
            };

            await _repositoryUserTask.Save(model);
        }

        private void Validate(TaskModel task)
        {
            var validation = new UserTaskValidation().Validate(task);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
