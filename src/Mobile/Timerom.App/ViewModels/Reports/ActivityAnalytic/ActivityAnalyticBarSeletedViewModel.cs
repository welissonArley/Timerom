using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.ViewModels.Reports.ActivityAnalytic
{
    public class ActivityAnalyticBarSeletedViewModel : ViewModelBase, IInitializeAsync
    {
        public string Description { get; private set; }

        public ActivityAnalyticStatisticsModel AnalyticModel { get; private set; }

        public ActivityAnalyticBarSeletedViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
        }

        public Task InitializeAsync(INavigationParameters parameters)
        {
            var date = parameters.GetValue<DateTime>("Date");

            Description = string.Format(ResourceText.TITLE_YOUR_ARE_SEEING_WHICH_CATEGORIES_MAKE_UP_THE_RESULT, date.ToString("D"));

            AnalyticModel = new ActivityAnalyticStatisticsModel
            {
                Productive = new TaskAnalyticModel
                {
                    AmountOfTasks = 5,
                    Time = new TimeSpan(0, 689, 0)
                },
                Neutral = new TaskAnalyticModel
                {
                    AmountOfTasks = 5,
                    Time = new TimeSpan(0, 123, 0)
                },
                Unproductive = new TaskAnalyticModel
                {
                    AmountOfTasks = 5,
                    Time = new TimeSpan(0, 78, 0)
                }
            };

            RaisePropertyChanged("Description");
            RaisePropertyChanged("AnalyticModel");

            return Task.CompletedTask;
        }
    }
}
