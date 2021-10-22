using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces
{
    public interface IActivityAnalyticDetailsSubCategoryUseCase
    {
        Task<ObservableCollection<ActivitiesAnalyticModel>> Execute(long subcategoryId, DateTime date);
    }
}
