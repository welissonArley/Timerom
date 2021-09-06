using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Dashboard.Interfaces;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;

namespace Timerom.App.ViewModels.Dashboard
{
    public class DashboardPageDetailViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IDashboardUseCase> useCase;
        private IDashboardUseCase _useCase => useCase.Value;

        public DashboardDateModel Model { get; set; }

        public IAsyncCommand<DateTime> DateChangedCommand { get; private set; }
        public IAsyncCommand ViewAllTasksCommand { get; private set; }

        public DashboardPageDetailViewModel(Lazy<IDashboardUseCase> useCase, Lazy<INavigationService> navigationService) : base(navigationService)
        {
            this.useCase = useCase;

            DateChangedCommand = new AsyncCommand<DateTime>(GetDashboard, onException: HandleException, allowsMultipleExecutions: false);
            ViewAllTasksCommand = new AsyncCommand(ViewAllTasksCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private async Task ViewAllTasksCommandExecuted()
        {
            await _navigationService.NavigateAsync(nameof(TaskDetailsPage));
        }

        private async Task GetDashboard(DateTime date)
        {
            Model = new DashboardDateModel
            {
                Date = date,
                Dashboard = await _useCase.Execute(date)
            };

            CurrentState = Model.Dashboard.TotalTasks == 0 ? LayoutState.Empty : LayoutState.None;

            RaisePropertyChanged("Model");
            RaisePropertyChanged("CurrentState");
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            await GetDashboard(DateTime.Now);
        }
    }
}
