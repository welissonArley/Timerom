﻿using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Dashboard.Interfaces;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;

namespace Timerom.App.ViewModels.Dashboard
{
    public class DashboardPageDetailViewModel : ViewModelBase, IInitializeAsync, INavigationAware
    {
        private readonly Lazy<IDashboardUseCase> useCase;
        private IDashboardUseCase _useCase => useCase.Value;

        public DashboardDateModel Model { get; set; }

        public IAsyncCommand<DateTime> DateChangedCommand { get; private set; }
        public IAsyncCommand ViewAllTasksCommand { get; private set; }
        public IAsyncCommand FloatActionCommand { get; private set; }

        public DashboardPageDetailViewModel(Lazy<IDashboardUseCase> useCase, Lazy<INavigationService> navigationService) : base(navigationService)
        {
            this.useCase = useCase;

            DateChangedCommand = new AsyncCommand<DateTime>(GetDashboard, onException: HandleException, allowsMultipleExecutions: false);
            ViewAllTasksCommand = new AsyncCommand(ViewAllTasksCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            FloatActionCommand = new AsyncCommand(FloatActionCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        /// <summary>
        /// This function need to send as parameter a Command to TaskDetailsPage use as a callback
        /// For some reason when update a task, Prism dont call OnNavigatedTo
        /// </summary>
        /// <returns></returns>
        private async Task ViewAllTasksCommandExecuted()
        {
            var navParameters = new NavigationParameters
            {
                { "CallbackUpdateUserTask", new AsyncCommand(() => GetDashboard(Model.Date)) },
                { "Date", Model.Date }
            };

            await _navigationService.NavigateAsync(nameof(TaskDetailsPage), navParameters);
        }

        private async Task FloatActionCommandExecuted()
        {
            _ = await _navigationService.NavigateAsync(nameof(SelectCategoryForTaskPage));
        }

        private async Task GetDashboard(DateTime date)
        {
            Model = new DashboardDateModel
            {
                Date = date,
                Dashboard = await _useCase.Execute(date)
            };

            CurrentState = Model.Dashboard.TotalTasks == 0 ? LayoutState.Empty : LayoutState.None;

            RaisePropertyChanged("Model");
            RaisePropertyChanged("CurrentState");
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            await GetDashboard(DateTime.Now);
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Refresh"))
                await GetDashboard(Model.Date);
        }
    }
}
