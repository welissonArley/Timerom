using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;

namespace Timerom.App.ViewModels.Dashboard
{
    public class DashboardPageDetailViewModel : ViewModelBase, IInitialize
    {
        public DashboardDateModel Model { get; set; }

        public IAsyncCommand ViewAllTasksCommand { get; private set; }

        public DashboardPageDetailViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            ViewAllTasksCommand = new AsyncCommand(ViewAllTasksCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private async Task ViewAllTasksCommandExecuted()
        {
            await _navigationService.NavigateAsync(nameof(TaskDetailsPage));
        }

        public void Initialize(INavigationParameters parameters)
        {
            Model = new DashboardDateModel
            {
                Date = DateTime.Today,
                Dashboard = new DashboardModel
                {
                    ProductivePercentage = 60,
                    NeutralPercentage = 25,
                    UnproductivePercentage = 15,
                    TotalTasks = 15,
                    Tasks = new System.Collections.ObjectModel.ObservableCollection<DashboardTaskModel>
                    {
                        new DashboardTaskModel
                        {
                            Title = "Sleep",
                            Hours = 8.5,
                            Percentage = 60,
                            Category = ValueObjects.Enuns.CategoryType.Productive
                        },
                        new DashboardTaskModel
                        {
                            Title = "Social Media",
                            Hours = 8.5,
                            Percentage = 25,
                            Category = ValueObjects.Enuns.CategoryType.Neutral
                        },
                        new DashboardTaskModel
                        {
                            Title = "Work",
                            Hours = 8.5,
                            Percentage = 15,
                            Category = ValueObjects.Enuns.CategoryType.Unproductive
                        }
                    }
                }
            };

            CurrentState = Model.Dashboard == null ? LayoutState.Empty : LayoutState.None;

            RaisePropertyChanged("Model");
            RaisePropertyChanged("CurrentState");
        }
    }
}
