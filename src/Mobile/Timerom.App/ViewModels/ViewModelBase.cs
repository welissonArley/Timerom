using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using Timerom.Exception.ExceptionBase;

namespace Timerom.App.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        private readonly Lazy<INavigationService> navigationService;
        protected INavigationService _navigationService => navigationService.Value;

        public ViewModelBase() { }

        public ViewModelBase(Lazy<INavigationService> navigationService)
        {
            this.navigationService = navigationService;
        }

        protected void HandleException(System.Exception exception)
        {
            NavigationParameters navParameters = new NavigationParameters
            {
                { "Messages", ShowException(exception) }
            };

            _navigationService.NavigateAsync(nameof(Views.Modal.OperationErrorModal), navParameters, useModalNavigation: true);
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
