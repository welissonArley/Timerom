using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.Exception.ExceptionBase;
using Xamarin.CommunityToolkit.UI.Views;

namespace Timerom.App.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        public LayoutState CurrentState { get; set; }

        private readonly Lazy<INavigationService> navigationService;
        protected INavigationService _navigationService => navigationService.Value;

        public ViewModelBase(Lazy<INavigationService> navigationService)
        {
            this.navigationService = navigationService;

            CurrentState = LayoutState.None;
        }

        protected void SavingStatus()
        {
            CurrentState = LayoutState.Saving;
            RaisePropertyChanged("CurrentState");
        }
        protected async Task SucessStatus(int millisecondsTimeout = 900)
        {
            CurrentState = LayoutState.Success;
            RaisePropertyChanged("CurrentState");

            await Task.Delay(millisecondsTimeout);

            NoneStatus();
        }
        private void NoneStatus()
        {
            CurrentState = LayoutState.None;
            RaisePropertyChanged("CurrentState");
        }

        protected void HandleException(System.Exception exception)
        {
            NavigationParameters navParameters = new NavigationParameters
            {
                { "Messages", ShowException(exception) }
            };

            _navigationService.NavigateAsync(nameof(Views.Modal.OperationErrorModal), navParameters, useModalNavigation: true);

            NoneStatus();
        }

        private List<string> ShowException(System.Exception exception)
        {
            if (exception is ErrorOnValidationException validacaoException)
                return validacaoException.ErrorMensages;
            else if (exception is TimeromException)
                return new List<string> { exception.Message };
            else
                return new List<string> { ResourceText.TITLE_UNKNOWN_ERROR };
        }
    }
}
