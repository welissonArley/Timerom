using Bogus;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using Timerom.App.Model;
using Timerom.App.ValueObjects.Enuns;

namespace Useful.ToTests.Builders.Request
{
    public class RequestCategory
    {
        private static RequestCategory _instance;

        public static RequestCategory Instance()
        {
            _instance = new RequestCategory();
            return _instance;
        }

        public Category Build()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, (f) => f.PickRandom<CategoryType>())
                .RuleFor(u => u.Childrens, () => ChildrensCategory());
        }

        private ObservableCollection<Category> ChildrensCategory()
        {
            var list = new ObservableCollection<Category>();

            var amount = RandomNumberGenerator.GetInt32(1, 7);

            for(var index = 0; index < amount; index++)
            {
                list.Add(new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, (f) => f.PickRandom<CategoryType>()));
            }

            return list;
        }
    }
}
