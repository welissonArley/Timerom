using System.Collections.Generic;
using System.Threading.Tasks;

namespace Timerom.App.UseCase.Categories.Interfaces
{
    public interface IGetAllCategoriesUseCase
    {
        Task<(IList<Model.Category> Productive, IList<Model.Category> Neutral, IList<Model.Category> Unproductive)> Execute();
    }
}
