using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Tasks
{
    public class TaskDetailsViewModel : ViewModelBase, IInitializeAsync, INavigationAware
    {
        private readonly Lazy<IGetAllUserTaskUseCase> useCase;
        private IGetAllUserTaskUseCase _useCase => useCase.Value;

        private ObservableCollection<TaskModel> _tasks { get; set; }
        public TasksDetailsModel Model { get; set; }

        public IAsyncCommand<string> SearchTextChangedCommand { get; private set; }
        public IAsyncCommand<DateTime> DateChangedCommand { get; private set; }
        public IAsyncCommand<TaskModel> SelectedItemCommand { get; private set; }
        public IAsyncCommand FilterListCommand { get; private set; }
        private IAsyncCommand _callbackUpdateUserTask { get; set; }

        public TaskDetailsViewModel(Lazy<INavigationService> navigationService, Lazy<IGetAllUserTaskUseCase> useCase) : base(navigationService)
        {
            this.useCase = useCase;
            SearchTextChangedCommand = new AsyncCommand<string>(OnSearchTextChanged);
            DateChangedCommand = new AsyncCommand<DateTime>(async (value) =>
            {
                await GetUserTasks(value, Model.Filters.Where(c => c.Checked).Select(c => c.Id).ToList());
            }, onException: HandleException, allowsMultipleExecutions: false);

            SelectedItemCommand = new AsyncCommand<TaskModel>(SelectedItemCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            FilterListCommand = new AsyncCommand(FilterListCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private Task OnSearchTextChanged(string value)
        {
            Model.Tasks = new ObservableCollection<TaskModel>(_tasks.Where(c => (!string.IsNullOrWhiteSpace(c.Description) && c.Description.ToUpper().Contains(value.ToUpper())) || c.Title.ToUpper().Contains(value.ToUpper())));

            RaisePropertyChanged("Model");

            return Task.CompletedTask;
        }

        private async Task GetUserTasks(DateTime date, IList<long> categoriesToFilterIds)
        {
            var model = await _useCase.Execute(date, categoriesToFilterIds);

            _tasks = new ObservableCollection<TaskModel>(model.Tasks);

            Model = model;

            RaisePropertyChanged("Model");
        }

        private async Task SelectedItemCommandExecuted(TaskModel task)
        {
            var navParameters = new NavigationParameters
            {
                { "Task", task }
            };

            await _navigationService.NavigateAsync(nameof(AddUpdateTaskPage), navParameters);
        }

        private async Task FilterListCommandExecuted()
        {
            await GetUserTasks(Model.Date, Model.Filters.Where(c => c.Checked).Select(c => c.Id).ToList());
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            _callbackUpdateUserTask = parameters.GetValue<AsyncCommand>("CallbackUpdateUserTask");
            var date = parameters.GetValue<DateTime>("Date");

            if (parameters.ContainsKey("CategoriesFilter"))
            {
                var categoriesFilter = parameters.GetValue<IList<long>>("CategoriesFilter");
                await GetUserTasks(date, categoriesFilter);
            }
            else
                await GetUserTasks(date, new List<long>());
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { /* We dont need this method, but it's necessary from interface */ }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Refresh"))
            {
                await GetUserTasks(Model.Date, Model.Filters.Where(c => c.Checked).Select(c => c.Id).ToList());
                _callbackUpdateUserTask?.Execute(null);
            }
        }

    }
}
