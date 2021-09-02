using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Views.Views.Dashboard;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Home
{
    public class HomePageViewModel : ViewModelBase
    {
        public IAsyncCommand HomeCommand { get; private set; }
        public IAsyncCommand AddTaskCommand { get; private set; }

        public HomePageViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            HomeCommand = new AsyncCommand(HomeCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            AddTaskCommand = new AsyncCommand(AddTaskCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private async Task HomeCommandExecuted()
        {
            _ = await _navigationService.NavigateAsync($"/DashboardPage/NavigationPage/{nameof(DashboardPageDetail)}");
        }
        private async Task AddTaskCommandExecuted()
        {
            _ = await _navigationService.NavigateAsync(nameof(SelectCategoryForTaskPage));
        }
    }
}
