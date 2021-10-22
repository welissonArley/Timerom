using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.UseCase.UserTask.Interfaces
{
    public interface IDeleteUserTaskUseCase
    {
        Task Execute(TaskModel task);
    }
}
