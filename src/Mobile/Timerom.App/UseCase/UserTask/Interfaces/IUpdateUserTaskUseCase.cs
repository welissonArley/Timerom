using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.UseCase.UserTask.Interfaces
{
    public interface IUpdateUserTaskUseCase
    {
        Task Execute(TaskModel task);
    }
}
