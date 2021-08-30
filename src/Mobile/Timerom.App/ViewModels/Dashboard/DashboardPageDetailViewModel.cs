using Prism.Navigation;
using System;
using Timerom.App.Model;

namespace Timerom.App.ViewModels.Dashboard
{
    public class DashboardPageDetailViewModel : ViewModelBase
    {
        public DashboardModel Model { get; set; }

        public DashboardPageDetailViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            Model = new DashboardModel
            {
                Date = DateTime.Today
            };
        }
    }
}
