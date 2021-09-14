using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Modal
{
    public class ConfirmActionModalViewModel : ViewModelBase, INavigationAware
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IAsyncCommand Action { get; set; }

        public IAsyncCommand CloseModalCommand { get; private set; }
        public IAsyncCommand IamSureCommand { get; private set; }

        public ConfirmActionModalViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            CloseModalCommand = new AsyncCommand(CloseModalCommandExecuted, allowsMultipleExecutions: false);
            IamSureCommand = new AsyncCommand(IamSureCommandExecuted, allowsMultipleExecutions: false);
        }

        private async Task CloseModalCommandExecuted()
        {
            await _navigationService.ClearPopupStackAsync();
        }

        private async Task IamSureCommandExecuted()
        {
            await CloseModalCommandExecuted();
            Action?.Execute(null);
        }

        public void OnNavigatedFrom(INavigationParameters parameters){}

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Title = parameters.GetValue<string>("Title");
            Description = parameters.GetValue<string>("Description");
            Action = parameters.GetValue<IAsyncCommand>("Action");

            RaisePropertyChanged("Title");
            RaisePropertyChanged("Description");
        }
    }
}
