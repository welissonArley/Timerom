using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.UseCase.Categories.Interfaces
{
    public interface IUpdateSubcategoryUseCase
    {
        Task Execute(Category category, long parentId);
    }
}
