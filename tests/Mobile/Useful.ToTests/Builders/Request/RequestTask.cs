using Bogus;
using System;
using Timerom.App.Model;
using Timerom.App.ValueObjects.Enuns;

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
                .RuleFor(u => u.EndsAt, () => DateTime.Now)
                .RuleFor(u => u.Description, (f) => f.Lorem.Paragraph())
                .RuleFor(u => u.Category, () => new Faker<Category>()
                    .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                    .RuleFor(u => u.Type, (f) => f.PickRandom<CategoryType>()));
        }
    }
}
