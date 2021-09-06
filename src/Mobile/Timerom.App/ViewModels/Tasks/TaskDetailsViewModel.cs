using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.UserTask.Interfaces;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Tasks
{
    public class TaskDetailsViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IGetAllUserTaskUseCase> useCase;
        private IGetAllUserTaskUseCase _useCase => useCase.Value;

        public TasksDetailsModel Model { get; set; }

        public IAsyncCommand<DateTime> DateChangedCommand { get; private set; }

        public TaskDetailsViewModel(Lazy<INavigationService> navigationService, Lazy<IGetAllUserTaskUseCase> useCase) : base(navigationService)
        {
            this.useCase = useCase;
            DateChangedCommand = new AsyncCommand<DateTime>(GetUserTasks, onException: HandleException, allowsMultipleExecutions: false);
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

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            await GetUserTasks(DateTime.Now);
        }
    }
}
