using Bogus;
using Timerom.App.ValueObjects.Entity;
using Timerom.App.ValueObjects.Enuns;

namespace Useful.ToTests.Builders.Entity
{
    public class CategoryEntityBuilder
    {
        private static CategoryEntityBuilder _instance;

        public static CategoryEntityBuilder Instance()
        {
            _instance = new CategoryEntityBuilder();
            return _instance;
        }

        public Category Build()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, (f) => f.PickRandom<CategoryType>());
        }

        public Category Productive()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Productive);
        }
        public Category Unproductive()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Unproductive);
        }
        public Category Neutral()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Neutral);
        }
    }
}
