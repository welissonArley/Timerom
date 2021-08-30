using Prism.Navigation;
using System;
using Timerom.App.Model;
using Xamarin.CommunityToolkit.UI.Views;

namespace Timerom.App.ViewModels.Dashboard
{
    public class DashboardPageDetailViewModel : ViewModelBase, IInitialize
    {
        public DashboardDateModel Model { get; set; }

        public DashboardPageDetailViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            
        }

        public void Initialize(INavigationParameters parameters)
        {
            Model = new DashboardDateModel
            {
                Date = DateTime.Today,
                Dashboard = new DashboardModel
                {
                    ProductivePercentage = 50,
                    NeutralPercentage = 50,
                    UnproductivePercentage = 0,
                    TotalTasks = 15
                }
            };

            CurrentState = Model.Dashboard == null ? LayoutState.Empty : LayoutState.None;

            RaisePropertyChanged("Model");
            RaisePropertyChanged("CurrentState");
        }
    }
}
