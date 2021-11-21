using System.Threading.Tasks;
using Timerom.App.ValueObjects.Entity;

namespace Timerom.App.Repository.Interface
{
    public interface IUserTaskWriteOnlyRepository
    {
        Task Delete(UserTask task);
        Task Save(UserTask task);
        Task Update(UserTask task);
    }
}
