using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces
{
    public interface IGetChartActivityAnalyticUseCase
    {
        Task<IList<ChartActivityAnalyticModel>> Execute();
    }
}
