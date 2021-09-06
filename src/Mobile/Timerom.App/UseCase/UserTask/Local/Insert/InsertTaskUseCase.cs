using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.UseCase.UserTask.Local.Insert
{
    public class InsertTaskUseCase : IInsertTaskUseCase
    {
        public async Task Execute(TaskModel task)
        {
            Validate(task);

            await Save(task);
        }

        private async Task Save(TaskModel task)
        {
            UserTaskDatabase database = await UserTaskDatabase.Instance();

            var model = new ValueObjects.Entity.UserTask
            {
                Title = task.Title,
                Description = task.Description,
                StartsAt = task.StartsAt,
                EndsAt = task.EndsAt,
                CategoryId = task.Category.Id
            };

            await database.Save(model);
        }

        private void Validate(TaskModel task)
        {
            var validation = new UserTaskValidation().Validate(task);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
