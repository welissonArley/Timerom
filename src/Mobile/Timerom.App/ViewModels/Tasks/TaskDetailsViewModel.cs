using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Tasks
{
    public class TaskDetailsViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IGetAllUserTaskUseCase> useCase;
        private IGetAllUserTaskUseCase _useCase => useCase.Value;

        public TasksDetailsModel Model { get; set; }

        public IAsyncCommand<DateTime> DateChangedCommand { get; private set; }
        public IAsyncCommand<TaskModel> SelectedItemCommand { get; private set; }

        public TaskDetailsViewModel(Lazy<INavigationService> navigationService, Lazy<IGetAllUserTaskUseCase> useCase) : base(navigationService)
        {
            this.useCase = useCase;
            DateChangedCommand = new AsyncCommand<DateTime>(GetUserTasks, onException: HandleException, allowsMultipleExecutions: false);
            SelectedItemCommand = new AsyncCommand<TaskModel>(SelectedItemCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private async Task GetUserTasks(DateTime date)
        {
            var models = await _useCase.Execute(date);

            Model = new TasksDetailsModel
            {
                Date = date,
                Tasks = new System.Collections.ObjectModel.ObservableCollection<TaskModel>(models)
            };

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

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            await GetUserTasks(DateTime.Now);
        }
    }
}
