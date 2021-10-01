using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;

namespace Timerom.App.ViewModels.Reports.ActivityAnalytic
{
    public class ActivityAnalyticViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IGetActivityAnalyticTotalUseCase> totalUseCase;
        private readonly Lazy<IGetChartActivityAnalyticUseCase> chartUseCase;
        private IGetActivityAnalyticTotalUseCase _totalUseCase => totalUseCase.Value;
        private IGetChartActivityAnalyticUseCase _chartUseCase => chartUseCase.Value;

        public ActivityAnalyticModel AnalyticModel { get; set; }
        public ObservableCollection<ChartActivityAnalyticModel> ChartModel { get; set; }

        public ActivityAnalyticViewModel(Lazy<INavigationService> navigationService,
            Lazy<IGetActivityAnalyticTotalUseCase> totalUseCase,
            Lazy<IGetChartActivityAnalyticUseCase> chartUseCase) : base(navigationService)
        {
            this.totalUseCase = totalUseCase;
            this.chartUseCase = chartUseCase;
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            AnalyticModel = await _totalUseCase.Execute();

            var chart = await _chartUseCase.Execute();
            ChartModel = new ObservableCollection<ChartActivityAnalyticModel>(chart);

            RaisePropertyChanged("AnalyticModel");
            RaisePropertyChanged("ChartModel");
        }
    }
}
