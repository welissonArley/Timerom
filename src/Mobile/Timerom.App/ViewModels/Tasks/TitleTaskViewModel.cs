using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Tasks
{
    public class TitleTaskViewModel : ViewModelBase, IInitialize
    {
        public string TitleTask { get; set; }

        public IAsyncCommand SaveCommand { get; private set; }
        private IAsyncCommand<string> CallbackCommand { get; set; }

        public TitleTaskViewModel(Lazy<INavigationService> navigationService): base(navigationService)
        {
            SaveCommand = new AsyncCommand(SaveCommandExecuted, allowsMultipleExecutions: false);
        }

        private async Task SaveCommandExecuted()
        {
            CallbackCommand?.Execute(TitleTask);
            await _navigationService.GoBackAsync();
        }

        public void Initialize(INavigationParameters parameters)
        {
            TitleTask = parameters.GetValue<string>("Title");
            CallbackCommand = parameters.GetValue<IAsyncCommand<string>>("Callback");

            RaisePropertyChanged("TitleTask");
        }
    }
}
