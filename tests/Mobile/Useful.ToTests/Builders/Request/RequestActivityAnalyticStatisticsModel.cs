using Bogus;
using System.Collections.ObjectModel;
using Timerom.App.Model;

namespace Useful.ToTests.Builders.Request
{
    public class RequestActivityAnalyticStatisticsModel
    {
        private static RequestActivityAnalyticStatisticsModel _instance;

        public static RequestActivityAnalyticStatisticsModel Instance()
        {
            _instance = new RequestActivityAnalyticStatisticsModel();
            return _instance;
        }

        public ActivityAnalyticStatisticsModel Build()
        {
            return new Faker<ActivityAnalyticStatisticsModel>()
                .RuleFor(u => u.Productive, (f) => new TaskAnalyticModel { Time = new System.TimeSpan(1,1,1), AmountOfTasks = f.Random.Number() })
                .RuleFor(u => u.Neutral, (f) => new TaskAnalyticModel { Time = new System.TimeSpan(1, 1, 1), AmountOfTasks = f.Random.Number() })
                .RuleFor(u => u.Unproductive, (f) => new TaskAnalyticModel { Time = new System.TimeSpan(1, 1, 1), AmountOfTasks = f.Random.Number() })
                .RuleFor(u => u.Activities, () => new ObservableCollection<ActivitiesAnalyticModel>
                {
                    RequestActivitiesAnalyticModel.Instance().Build()
                });
        }
    }
}
