using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Views.Reports.ActivityAnalytic;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Reports.ActivityAnalytic
{
    public class ActivityAnalyticViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IGetActivityAnalyticTotalUseCase> totalUseCase;
        private readonly Lazy<IGetChartActivityAnalyticUseCase> chartUseCase;
        private IGetActivityAnalyticTotalUseCase _totalUseCase => totalUseCase.Value;
        private IGetChartActivityAnalyticUseCase _chartUseCase => chartUseCase.Value;

        public IAsyncCommand<DateTime> DaySelectedCommand { get; private set; }

        public ActivityAnalyticModel AnalyticModel { get; set; }
        public ObservableCollection<ChartActivityAnalyticModel> ChartModel { get; set; }

        public ActivityAnalyticViewModel(Lazy<INavigationService> navigationService,
            Lazy<IGetActivityAnalyticTotalUseCase> totalUseCase,
            Lazy<IGetChartActivityAnalyticUseCase> chartUseCase) : base(navigationService)
        {
            this.totalUseCase = totalUseCase;
            this.chartUseCase = chartUseCase;

            DaySelectedCommand = new AsyncCommand<DateTime>(DaySelectedCommandExecuted, allowsMultipleExecutions: false);
        }

        private async Task DaySelectedCommandExecuted(DateTime date)
        {
            TrackEvent("ActivityAnalyticPage", "BarSelected", EventFlag.Navigation);

            var navParameters = new NavigationParameters
            {
                { "Date", date }
            };

            await _navigationService.NavigateAsync(nameof(ActivityAnalyticBarSeletedPage), navParameters);
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            AnalyticModel = await _totalUseCase.Execute();

            var chart = await _chartUseCase.Execute();
            ChartModel = new ObservableCollection<ChartActivityAnalyticModel>(chart);

            if (ChartModel.Any())
            {
                CurrentState = Xamarin.CommunityToolkit.UI.Views.LayoutState.None;

                RaisePropertyChanged("AnalyticModel");
                RaisePropertyChanged("ChartModel");
            }
            else
                CurrentState = Xamarin.CommunityToolkit.UI.Views.LayoutState.Empty;

            RaisePropertyChanged("CurrentState");
        }
    }
}
