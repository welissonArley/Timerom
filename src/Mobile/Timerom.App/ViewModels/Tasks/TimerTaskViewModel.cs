using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Services.BackGroundService;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Timerom.App.ViewModels.Tasks
{
    public class TimerTaskViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<ITimerUserTask> timerUserTask;
        private ITimerUserTask _timerUserTask => timerUserTask.Value;
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

        public TimerTaskViewModel(Lazy<INavigationService> navigationService, Lazy<IGetByIdCategoryUseCase> useCase,
            Lazy<ITimerUserTask> timerUserTask) : base(navigationService)
        {
            this.useCase = useCase;
            this.timerUserTask = timerUserTask;

            StartTimerCommand = new AsyncCommand(StartTimeCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            StopTimerCommand = new AsyncCommand(StopTimeCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            AddTaskTitleCommand = new AsyncCommand(AddTaskTitleCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private Task StartTimeCommandExecuted()
        {
            TrackEvent("TimerTaskPage", "Start", EventFlag.Click);

            StartBackgroundService_Properties();

            var navigation = GetNavigation();

            navigation.RemovePage(navigation.NavigationStack[1]);
            navigation.RemovePage(navigation.NavigationStack[1]);

            return Task.CompletedTask;
        }

        private void StartBackgroundService_Properties()
        {
            Subscribe();

            _timerUserTask.StartJob(Subcategory);

            IsRunning = true;
            RaisePropertyChanged("IsRunning");
        }

        private async Task StopTimeCommandExecuted()
        {
            var timerStartsAt = _timerUserTask.TimerStartsAt();

            _timerUserTask.StopJob();

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
            TrackEvent("TimerTaskPage", "TitleTaskPage", EventFlag.Navigation);

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

            _timerUserTask.SetTitle(Title);

            RaisePropertyChanged("Title");

            return Task.CompletedTask;
        }

        private void Subscribe()
        {
            _timerUserTask.Subscribe(new Command((seconds) =>
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
            if (_timerUserTask.IsRunning())
            {
                _totalSeconds = _timerUserTask.GetTotalSeconds();
                Time = DateTime.Now.Date.AddSeconds(_totalSeconds);
                RaisePropertyChanged("Time");
                
                Subcategory = await _useCase.Execute(_timerUserTask.SubcategoryId());
                Title = _timerUserTask.GetTitle();
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
