using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.UserTask.Local.Update
{
    public class UpdateUserTaskUseCase : IUpdateUserTaskUseCase
    {
        public async Task Execute(TaskModel task)
        {
            Validate(task);

            await Save(task);
        }

        private async Task Save(TaskModel task)
        {
            UserTaskDatabase database = await UserTaskDatabase.Instance();
            ValueObjects.Entity.UserTask taskModel = await database.GetById(task.Id);

            taskModel.Title = task.Title;
            taskModel.Description = task.Description;
            taskModel.EndsAt = task.EndsAt;
            taskModel.StartsAt = task.StartsAt;

            await database.Update(taskModel);
        }

        private void Validate(TaskModel task)
        {
            var validation = new UserTaskValidation().Validate(task);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
