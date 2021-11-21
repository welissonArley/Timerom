using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.UserTask.Local.Update
{
    public class UpdateUserTaskUseCase : IUpdateUserTaskUseCase
    {
        private readonly Lazy<IUserTaskReadOnlyRepository> repositoryReadOnly;
        private readonly Lazy<IUserTaskWriteOnlyRepository> repository;
        private IUserTaskWriteOnlyRepository _repositoryUserTask => repository.Value;
        private IUserTaskReadOnlyRepository _repositoryReadOnly => repositoryReadOnly.Value;

        public UpdateUserTaskUseCase(Lazy<IUserTaskWriteOnlyRepository> repository, Lazy<IUserTaskReadOnlyRepository> repositoryReadOnly)
        {
            this.repository = repository;
            this.repositoryReadOnly = repositoryReadOnly;
        }

        public async Task Execute(TaskModel task)
        {
            Validate(task);

            await Save(task);
        }

        private async Task Save(TaskModel task)
        {
            ValueObjects.Entity.UserTask taskModel = await _repositoryReadOnly.GetById(task.Id);

            taskModel.Title = task.Title;
            taskModel.Description = task.Description;
            taskModel.EndsAt = task.EndsAt;
            taskModel.StartsAt = task.StartsAt;

            await _repositoryUserTask.Update(taskModel);
        }

        private void Validate(TaskModel task)
        {
            var validation = new UserTaskValidation().Validate(task);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
