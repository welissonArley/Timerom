using Bogus;
using System;
using Timerom.App.Model;

namespace Useful.ToTests.Builders.Request
{
    public class RequestActivityAnalyticModel
    {
        private static RequestActivityAnalyticModel _instance;

        public static RequestActivityAnalyticModel Instance()
        {
            _instance = new RequestActivityAnalyticModel();
            return _instance;
        }

        public ActivityAnalyticModel Build()
        {
            return new Faker<ActivityAnalyticModel>()
                .RuleFor(u => u.Total, (f) => new TaskAnalyticModel { Time = new TimeSpan(1,1,1), AmountOfTasks = f.Random.Number() })
                .RuleFor(u => u.Productive, (f) => new TaskAnalyticModel { Time = new TimeSpan(1, 1, 1), AmountOfTasks = f.Random.Number() })
                .RuleFor(u => u.Neutral, (f) => new TaskAnalyticModel { Time = new TimeSpan(1, 1, 1), AmountOfTasks = f.Random.Number() })
                .RuleFor(u => u.Unproductive, (f) => new TaskAnalyticModel { Time = new TimeSpan(1, 1, 1), AmountOfTasks = f.Random.Number() });
        }
    }
}
