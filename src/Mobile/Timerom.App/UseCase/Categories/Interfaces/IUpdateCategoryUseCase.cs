using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.UseCase.Categories.Interfaces
{
    public interface IUpdateCategoryUseCase
    {
        Task<Category> Execute(Category category);
    }
}
