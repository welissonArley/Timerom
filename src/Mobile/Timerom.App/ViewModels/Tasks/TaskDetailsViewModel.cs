using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.UserTask.Interfaces;

namespace Timerom.App.ViewModels.Tasks
{
    public class TaskDetailsViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IGetAllUserTaskUseCase> useCase;
        private IGetAllUserTaskUseCase _useCase => useCase.Value;

        public TasksDetailsModel Model { get; set; }

        public TaskDetailsViewModel(Lazy<INavigationService> navigationService, Lazy<IGetAllUserTaskUseCase> useCase) : base(navigationService)
        {
            this.useCase = useCase;
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var models = await _useCase.Execute(DateTime.Today);

            Model = new TasksDetailsModel
            {
                Date = DateTime.Today,
                Tasks = new System.Collections.ObjectModel.ObservableCollection<TaskModel>(models)
            };

            RaisePropertyChanged("Model");
        }
    }
}
