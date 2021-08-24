using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;

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

        protected void HandleException(Exception exception)
        {
            NavigationParameters navParameters = new NavigationParameters
            {
                { "Messages", ShowException(exception) }
            };

            _navigationService.NavigateAsync(nameof(Views.Modal.OperationErrorModal), navParameters, useModalNavigation: true);
        }

        private List<string> ShowException(Exception exception)
        {
            return new List<string> { exception.Message };
        }
    }
}
