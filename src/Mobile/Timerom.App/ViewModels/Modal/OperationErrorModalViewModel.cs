using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Modal
{
    public class OperationErrorModalViewModel : ViewModelBase, INavigationAware
    {
        public IList<string> Messages { get; set; }

        public IAsyncCommand CloseModalCommand { get; private set; }

        public OperationErrorModalViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            CloseModalCommand = new AsyncCommand(CloseModalCommandExecuted, allowsMultipleExecutions: false);
        }

        private async Task CloseModalCommandExecuted()
        {
            await _navigationService.ClearPopupStackAsync();
        }

        public void OnNavigatedFrom(INavigationParameters parameters){}

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Messages = parameters.GetValue<IList<string>>("Messages");
            RaisePropertyChanged("Messages");
        }
    }
}
