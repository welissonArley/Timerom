using System.Threading.Tasks;

namespace Timerom.App.UseCase.Categories.Interfaces
{
    public interface IGetByIdCategoryUseCase
    {
        Task<Model.Category> Execute(long id);
    }
}
