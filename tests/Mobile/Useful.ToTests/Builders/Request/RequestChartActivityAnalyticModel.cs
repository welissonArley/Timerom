using Bogus;
using System;
using Timerom.App.Model;
using Timerom.App.ValueObjects.Enuns;

namespace Useful.ToTests.Builders.Request
{
    public class RequestChartActivityAnalyticModel
    {
        private static RequestChartActivityAnalyticModel _instance;

        public static RequestChartActivityAnalyticModel Instance()
        {
            _instance = new RequestChartActivityAnalyticModel();
            return _instance;
        }

        public ChartActivityAnalyticModel Build()
        {
            return new Faker<ChartActivityAnalyticModel>()
                .RuleFor(u => u.Date, () => DateTime.Now)
                .RuleFor(u => u.Time, (f) => new TimeSpan(1, 1, 1))
                .RuleFor(u => u.Type, (f) => f.PickRandom<CategoryType>());
        }
    }
}
