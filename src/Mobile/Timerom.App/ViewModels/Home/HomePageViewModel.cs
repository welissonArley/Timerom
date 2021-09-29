using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Services.BackGroundService;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Views.Dashboard;
using Timerom.App.Views.Views.Reports.ActivityAnalytic;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Home
{
    public class HomePageViewModel : ViewModelBase, INavigationAware
    {
        public IAsyncCommand HomeCommand { get; private set; }
        public IAsyncCommand AddTaskCommand { get; private set; }
        public IAsyncCommand StartTaskCommand { get; private set; }
        public IAsyncCommand ShowReportCommand { get; private set; }

        public bool ThereIsTimer { get; private set; }

        public HomePageViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            HomeCommand = new AsyncCommand(HomeCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            AddTaskCommand = new AsyncCommand(AddTaskCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            StartTaskCommand = new AsyncCommand(StartTaskCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            ShowReportCommand = new AsyncCommand(ShowReportCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private async Task HomeCommandExecuted()
        {
            _ = await _navigationService.NavigateAsync($"/DashboardPage/NavigationPage/{nameof(DashboardPageDetail)}");
        }
        private async Task AddTaskCommandExecuted()
        {
            var navParameters = new NavigationParameters
            {
                { "Option", OnSelectCategoryOptions.AddTask }
            };
            _ = await _navigationService.NavigateAsync(nameof(SelectCategoryForTaskPage), navParameters);
        }
        private async Task StartTaskCommandExecuted()
        {
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
            _ = await _navigationService.NavigateAsync(nameof(ActivityAnalyticPage));
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ThereIsTimer = TimerUserTaskService.IsRunning();

            RaisePropertyChanged("ThereIsTimer");
        }
    }
}
