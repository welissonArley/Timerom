using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.ValueObjects.Enuns;
using Timerom.Exception.ExceptionBase;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

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
            Crashes.TrackError(exception);

            NavigationParameters navParameters = new NavigationParameters
            {
                { "Messages", ShowException(exception) }
            };

            _navigationService.NavigateAsync(nameof(Views.Modal.OperationErrorModal), navParameters, useModalNavigation: true);

            NoneStatus();
        }

        protected INavigation GetNavigation()
        {
            if (Application.Current.MainPage is Views.Views.Dashboard.DashboardPage)
                return (Application.Current.MainPage as Views.Views.Dashboard.DashboardPage).Detail.Navigation;

            return Application.Current.MainPage.Navigation;
        }

        protected void TrackEvent(string pageName, string eventName, EventFlag flag)
        {
            var properties = new Dictionary<string, string>
            {
                { Enum.GetName(typeof(EventFlag), flag), eventName }
            };

            Analytics.TrackEvent(pageName, properties);
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
