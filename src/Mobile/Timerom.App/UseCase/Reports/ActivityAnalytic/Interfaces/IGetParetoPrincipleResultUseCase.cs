using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.ValueObjects.Dto;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces
{
    public interface IGetParetoPrincipleResultUseCase
    {
        Task<ParetoPrincipleModel> Execute(ParetoPrincipleFilter filter);
    }
}
