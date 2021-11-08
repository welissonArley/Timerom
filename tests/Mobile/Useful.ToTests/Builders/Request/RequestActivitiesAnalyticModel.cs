using Bogus;
using Timerom.App.Model;
using Timerom.App.ValueObjects.Enuns;

namespace Useful.ToTests.Builders.Request
{
    public class RequestActivitiesAnalyticModel
    {
        private static RequestActivitiesAnalyticModel _instance;

        public static RequestActivitiesAnalyticModel Instance()
        {
            _instance = new RequestActivitiesAnalyticModel();
            return _instance;
        }

        public ActivitiesAnalyticModel Build()
        {
            return new Faker<ActivitiesAnalyticModel>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Time, () => new System.TimeSpan(1,1,1))
                .RuleFor(u => u.Type, (f) => f.PickRandom<CategoryType>())
                .RuleFor(u => u.Percentage, (f) => f.Random.Double(1, 100));
        }
    }
}
