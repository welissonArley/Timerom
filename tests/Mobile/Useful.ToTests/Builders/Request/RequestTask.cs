using Bogus;
using System;
using Timerom.App.Model;

namespace Useful.ToTests.Builders.Request
{
    public class RequestTask
    {
        private static RequestTask _instance;

        public static RequestTask Instance()
        {
            _instance = new RequestTask();
            return _instance;
        }

        public TaskModel Build()
        {
            return new Faker<TaskModel>()
                .RuleFor(u => u.Title, (f) => f.Internet.UserName())
                .RuleFor(u => u.StartsAt, () => DateTime.Now.AddHours(-2))
                .RuleFor(u => u.EndsAt, () => DateTime.Now);
        }
    }
}
