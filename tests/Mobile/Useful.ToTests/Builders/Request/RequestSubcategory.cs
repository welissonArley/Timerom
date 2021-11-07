using Bogus;
using Timerom.App.Model;
using Timerom.App.ValueObjects.Enuns;

namespace Useful.ToTests.Builders.Request
{
    public class RequestSubcategory
    {
        private static RequestSubcategory _instance;

        public static RequestSubcategory Instance()
        {
            _instance = new RequestSubcategory();
            return _instance;
        }

        public Category Build()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, (f) => f.PickRandom<CategoryType>());
        }
    }
}
