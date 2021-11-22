using Bogus;
using Timerom.App.ValueObjects.Entity;
using Timerom.App.ValueObjects.Enuns;

namespace Useful.ToTests.Builders.Entity
{
    public class SubcategoryEntityBuilder
    {
        private static SubcategoryEntityBuilder _instance;

        public static SubcategoryEntityBuilder Instance()
        {
            _instance = new SubcategoryEntityBuilder();
            return _instance;
        }

        public Category Build()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, (f) => f.PickRandom<CategoryType>())
                .RuleFor(u => u.ParentCategoryId, (f) => f.Random.Number(1, 100));
        }

        public Category Productive()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Productive)
                .RuleFor(u => u.ParentCategoryId, (f) => f.Random.Number(1, 100));
        }
        public Category Unproductive()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Unproductive)
                .RuleFor(u => u.ParentCategoryId, (f) => f.Random.Number(1, 100));
        }
        public Category Neutral()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Neutral)
                .RuleFor(u => u.ParentCategoryId, (f) => f.Random.Number(1, 100));
        }
    }
}
