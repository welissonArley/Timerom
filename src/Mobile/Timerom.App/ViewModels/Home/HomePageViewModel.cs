using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Services.BackGroundService;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Views.Category;
using Timerom.App.Views.Views.Dashboard;
using Timerom.App.Views.Views.Reports.ActivityAnalytic;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Home
{
    public class HomePageViewModel : ViewModelBase, INavigationAware
    {
        private readonly Lazy<ITimerUserTask> timerUserTask;
        private ITimerUserTask _timerUserTask => timerUserTask.Value;

        public IAsyncCommand HomeCommand { get; private set; }
        public IAsyncCommand AddTaskCommand { get; private set; }
        public IAsyncCommand StartTaskCommand { get; private set; }
        public IAsyncCommand ShowReportCommand { get; private set; }
        public IAsyncCommand ShowCategoriesCommand { get; private set; }

        public bool ThereIsTimer { get; private set; }

        public HomePageViewModel(Lazy<INavigationService> navigationService, Lazy<ITimerUserTask> timerUserTask) : base(navigationService)
        {
            this.timerUserTask = timerUserTask;

            HomeCommand = new AsyncCommand(HomeCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            AddTaskCommand = new AsyncCommand(AddTaskCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            StartTaskCommand = new AsyncCommand(StartTaskCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            ShowReportCommand = new AsyncCommand(ShowReportCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            ShowCategoriesCommand = new AsyncCommand(ShowCategoriesCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private async Task HomeCommandExecuted()
        {
            TrackEvent("HomePage", "DashboardPageDetail", EventFlag.Navigation);
            _ = await _navigationService.NavigateAsync($"/DashboardPage/NavigationPage/{nameof(DashboardPageDetail)}");
        }
        private async Task AddTaskCommandExecuted()
        {
            TrackEvent("HomePage", "AddTask", EventFlag.Navigation);

            var navParameters = new NavigationParameters
            {
                { "Option", OnSelectCategoryOptions.AddTask }
            };
            _ = await _navigationService.NavigateAsync(nameof(SelectCategoryForTaskPage), navParameters);
        }
        private async Task StartTaskCommandExecuted()
        {
            TrackEvent("HomePage", "StartTimer", EventFlag.Navigation);

            if (ThereIsTimer)
                _ = await _navigationService.NavigateAsync(nameof(TimerTaskPage));
            else
            {
                var navParameters = new NavigationParameters
                {
                    { "Option", OnSelectCategoryOptions.Timer }
                };
                _ = await _navigationService.NavigateAsync(nameof(SelectCategoryForTaskPage), navParameters);
            }
        }
        private async Task ShowReportCommandExecuted()
        {
            TrackEvent("HomePage", "ActivityAnalyticPage", EventFlag.Navigation);
            _ = await _navigationService.NavigateAsync(nameof(ActivityAnalyticPage));
        }
        private async Task ShowCategoriesCommandExecuted()
        {
            TrackEvent("HomePage", "CategoriesPage", EventFlag.Navigation);
            _ = await _navigationService.NavigateAsync(nameof(CategoriesPage));
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { /* We dont need this method, but it's necessary from interface */ }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ThereIsTimer = _timerUserTask.IsRunning();

            RaisePropertyChanged("ThereIsTimer");
        }
    }
}
