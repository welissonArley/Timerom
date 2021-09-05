using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.UseCase.UserTask.Interfaces
{
    public interface IInsertTaskUseCase
    {
        Task Execute(TaskModel task);
    }
}
