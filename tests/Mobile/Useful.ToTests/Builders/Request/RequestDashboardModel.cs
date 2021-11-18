using Bogus;
using System.Collections.ObjectModel;
using Timerom.App.Model;

namespace Useful.ToTests.Builders.Request
{
    public class RequestDashboardModel
    {
        private static RequestDashboardModel _instance;

        public static RequestDashboardModel Instance()
        {
            _instance = new RequestDashboardModel();
            return _instance;
        }

        public DashboardModel Build()
        {
            return new Faker<DashboardModel>()
                .RuleFor(u => u.NeutralPercentage, (f) => f.Random.Number(0, 100))
                .RuleFor(u => u.ProductivePercentage, (f) => f.Random.Number(0, 100))
                .RuleFor(u => u.UnproductivePercentage, (f) => f.Random.Number(0, 100))
                .RuleFor(u => u.TotalTasks, (f) => f.Random.Number(0, 7))
                .RuleFor(u => u.Tasks, (f) => new ObservableCollection<DashboardTaskModel>
                {
                    new DashboardTaskModel
                    {
                        Title = "Task test",
                        Hours = new System.TimeSpan(1,1,1),
                        Percentage = 7
                    }
                });
        }
    }
}
