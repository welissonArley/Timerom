using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.UserTask.Interfaces;

namespace Timerom.App.UseCase.UserTask.Local.Delete
{
    public class DeleteUserTaskUseCase : IDeleteUserTaskUseCase
    {
        public async Task Execute(TaskModel task)
        {
            UserTaskDatabase database = await UserTaskDatabase.Instance();

            ValueObjects.Entity.UserTask taskModel = await database.GetById(task.Id);

            await database.Delete(taskModel);
        }
    }
}
