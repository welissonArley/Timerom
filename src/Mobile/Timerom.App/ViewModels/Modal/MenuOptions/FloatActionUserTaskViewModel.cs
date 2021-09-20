using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Services.BackGroundService;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Modal.MenuOptions
{
    public class FloatActionUserTaskViewModel : ViewModelBase, IInitialize
    {
        public bool ThereIsTimer { get; private set; }

        public IAsyncCommand AddUserTaskCommand { get; private set; }
        public IAsyncCommand StartUserTaskCommand { get; private set; }

        public FloatActionUserTaskViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            AddUserTaskCommand = new AsyncCommand(AddUserTaskCommandExecuted, allowsMultipleExecutions: false);
            StartUserTaskCommand = new AsyncCommand(StartUserTaskCommandExecuted, allowsMultipleExecutions: false);
        }

        private async Task AddUserTaskCommandExecuted()
        {
            var navParameters = new NavigationParameters
            {
                { "Option", OnSelectCategoryOptions.AddTask }
            };

            await Navigate(navParameters);
        }
        private async Task StartUserTaskCommandExecuted()
        {
            await _navigationService.ClearPopupStackAsync();

            var navParameters = new NavigationParameters
            {
                { "Option", OnSelectCategoryOptions.Timer }
            };

            await Navigate(navParameters);
        }

        private async Task Navigate(NavigationParameters navParameters)
        {
            if (ThereIsTimer)
                _ = await _navigationService.NavigateAsync(nameof(TimerTaskPage));
            else
                _ = await _navigationService.NavigateAsync(nameof(SelectCategoryForTaskPage), navParameters);
        }

        public void Initialize(INavigationParameters parameters)
        {
            ThereIsTimer = TimerUserTaskService.IsRunning();

            RaisePropertyChanged("ThereIsTimer");
        }
    }
}
