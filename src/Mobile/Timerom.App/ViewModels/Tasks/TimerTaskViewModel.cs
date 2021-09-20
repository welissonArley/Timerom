using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Services.BackGroundService;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Timerom.App.ViewModels.Tasks
{
    public class TimerTaskViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IGetByIdCategoryUseCase> useCase;
        private IGetByIdCategoryUseCase _useCase => useCase.Value;

        public bool IsRunning { get; private set; }
        public DateTime Time { get; set; }
        private int _totalSeconds { get; set; }
        public Model.Category Subcategory { get; set; }
        public string Title { get; set; }

        public IAsyncCommand StartTimerCommand { get; private set; }
        public IAsyncCommand StopTimerCommand { get; private set; }
        public IAsyncCommand AddTaskTitleCommand { get; private set; }

        private readonly TimerUserTaskService _timerUserTaskService;

        public TimerTaskViewModel(Lazy<INavigationService> navigationService, Lazy<IGetByIdCategoryUseCase> useCase) : base(navigationService)
        {
            this.useCase = useCase;

            _timerUserTaskService = new TimerUserTaskService();

            StartTimerCommand = new AsyncCommand(StartTimeCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            StopTimerCommand = new AsyncCommand(StopTimeCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            AddTaskTitleCommand = new AsyncCommand(AddTaskTitleCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private Task StartTimeCommandExecuted()
        {
            StartBackgroundService_Properties();

            var navigation = GetNavigation();

            navigation.RemovePage(navigation.NavigationStack[1]);
            navigation.RemovePage(navigation.NavigationStack[1]);

            return Task.CompletedTask;
        }

        private void StartBackgroundService_Properties()
        {
            Subscribe();

            _timerUserTaskService.StartJob(Subcategory);

            IsRunning = true;
            RaisePropertyChanged("IsRunning");
        }

        private async Task StopTimeCommandExecuted()
        {
            var timerStartsAt = _timerUserTaskService.TimerStartsAt();

            _timerUserTaskService.StopJob();

            var navParameters = new NavigationParameters
            {
                { "Task", new TaskModel { Title = Title.Equals(ResourceText.TITLE_CLICK_HERE_FILL_TASK_TITLE) ? "" : Title, Category = Subcategory, StartsAt = timerStartsAt, EndsAt = timerStartsAt.AddSeconds(_totalSeconds) } }
            };

            await _navigationService.NavigateAsync(nameof(AddUpdateTaskPage), navParameters);

            var navigation = GetNavigation();
            navigation.RemovePage(navigation.NavigationStack[1]);
        }

        private async Task AddTaskTitleCommandExecuted()
        {
            var navParameters = new NavigationParameters
            {
                { "Title", Title.Equals(ResourceText.TITLE_CLICK_HERE_FILL_TASK_TITLE) ? "" : Title },
                { "Callback", new AsyncCommand<string>(UpdateTaskTitle)}
            };

            await _navigationService.NavigateAsync(nameof(TitleTaskPage), navParameters);
        }

        private Task UpdateTaskTitle(string title)
        {
            Title = string.IsNullOrWhiteSpace(title) ? ResourceText.TITLE_CLICK_HERE_FILL_TASK_TITLE : title;

            _timerUserTaskService.SetTitle(Title);

            RaisePropertyChanged("Title");

            return Task.CompletedTask;
        }

        private void Subscribe()
        {
            _timerUserTaskService.Subscribe(new Command((seconds) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    _totalSeconds = (int)seconds;
                    Time = DateTime.Now.Date.AddSeconds(_totalSeconds);
                    RaisePropertyChanged("Time");
                });
            }));
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            if (TimerUserTaskService.IsRunning())
            {
                _totalSeconds = _timerUserTaskService.GetTime();
                Time = DateTime.Now.Date.AddSeconds(_totalSeconds);
                RaisePropertyChanged("Time");
                
                Subcategory = await _useCase.Execute(_timerUserTaskService.SubcategoryId());
                Title = _timerUserTaskService.GetTitle();
                Title = string.IsNullOrWhiteSpace(Title) ? ResourceText.TITLE_CLICK_HERE_FILL_TASK_TITLE : Title;
                StartBackgroundService_Properties();
            }
            else
            {
                Title = ResourceText.TITLE_CLICK_HERE_FILL_TASK_TITLE;
                Subcategory = parameters.GetValue<Model.Category>("Subcategory");
            }

            RaisePropertyChanged("Title");
            RaisePropertyChanged("Subcategory");
        }
    }
}
