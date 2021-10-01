using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces
{
    public interface IGetActivityAnalyticTotalUseCase
    {
        Task<ActivityAnalyticModel> Execute();
    }
}
