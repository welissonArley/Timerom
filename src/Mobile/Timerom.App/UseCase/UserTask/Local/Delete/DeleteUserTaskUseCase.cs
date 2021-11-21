using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.UserTask.Interfaces;

namespace Timerom.App.UseCase.UserTask.Local.Delete
{
    public class DeleteUserTaskUseCase : IDeleteUserTaskUseCase
    {
        private readonly Lazy<IUserTaskReadOnlyRepository> repositoryReadOnly;
        private readonly Lazy<IUserTaskWriteOnlyRepository> repository;
        private IUserTaskReadOnlyRepository _repositoryReadOnly => repositoryReadOnly.Value;
        private IUserTaskWriteOnlyRepository _repository => repository.Value;

        public DeleteUserTaskUseCase(Lazy<IUserTaskReadOnlyRepository> repositoryReadOnly, Lazy<IUserTaskWriteOnlyRepository> repository)
        {
            this.repositoryReadOnly = repositoryReadOnly;
            this.repository = repository;
        }

        public async Task Execute(TaskModel task)
        {
            ValueObjects.Entity.UserTask taskModel = await _repositoryReadOnly.GetById(task.Id);

            await _repository.Delete(taskModel);
        }
    }
}
