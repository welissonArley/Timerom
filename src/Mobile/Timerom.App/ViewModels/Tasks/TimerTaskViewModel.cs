using Matcha.BackgroundService;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Services.BackGroundService;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Timerom.App.ViewModels.Tasks
{
    public class TimerTaskViewModel : ViewModelBase, IInitialize
    {
        public bool IsRunning { get; private set; }
        public DateTime Time { get; set; }
        private int _totalSeconds { get; set; }
        public Model.Category Subcategory { get; set; }
        public string Title { get; set; }

        public IAsyncCommand StartTimerCommand { get; private set; }
        public IAsyncCommand StopTimerCommand { get; private set; }
        public IAsyncCommand AddTaskTitleCommand { get; private set; }

        public TimerTaskViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            StartTimerCommand = new AsyncCommand(StartTimeCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            StopTimerCommand = new AsyncCommand(StopTimeCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            AddTaskTitleCommand = new AsyncCommand(AddTaskTitleCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private Task StartTimeCommandExecuted()
        {
            BackgroundAggregatorService.Add(() => new TimerUserTaskService(1, Subcategory));

            StartBackgroundService_Properties();

            Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[1]);
            Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[1]);

            return Task.CompletedTask;
        }

        private void StartBackgroundService_Properties()
        {
            Subscribe();

            BackgroundAggregatorService.StartBackgroundService();

            IsRunning = true;
            RaisePropertyChanged("IsRunning");
        }

        private async Task StopTimeCommandExecuted()
        {
            var timerStartsAt = TimerUserTaskService.TimerStartsAt();

            BackgroundAggregatorService.StopBackgroundService();
            BackgroundAggregatorService.Instance.Clear();

            TimerUserTaskService.StopJob();

            var navParameters = new NavigationParameters
            {
                { "Task", new TaskModel { Title = Title.Equals(ResourceText.TITLE_CLICK_HERE_FILL_TASK_TITLE) ? "" : Title, Category = Subcategory, StartsAt = timerStartsAt, EndsAt = timerStartsAt.AddSeconds(_totalSeconds) } }
            };

            await _navigationService.NavigateAsync(nameof(AddUpdateTaskPage), navParameters);

            Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[1]);
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

            TimerUserTaskService.SetTitle(Title);

            RaisePropertyChanged("Title");

            return Task.CompletedTask;
        }

        private void Subscribe()
        {
            TimerUserTaskService.Subscribe(new Command((seconds) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    _totalSeconds = (int)seconds;
                    Time = DateTime.Now.Date.AddSeconds(_totalSeconds);
                    RaisePropertyChanged("Time");
                });
            }));
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (TimerUserTaskService.IsRunning())
            {
                Subcategory = TimerUserTaskService.Subcategory();
                Title = TimerUserTaskService.GetTitle();
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
