using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.UseCase.Categories.Interfaces
{
    public interface IDeleteCategoryUseCase
    {
        Task Execute(Category category);
    }
}
