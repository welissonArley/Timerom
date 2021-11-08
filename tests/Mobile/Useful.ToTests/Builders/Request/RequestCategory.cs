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
                .RuleFor(u => u.Childrens, (_, u) => ChildrensCategory(u));
        }

        public Category Productive()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Productive)
                .RuleFor(u => u.Childrens, (_, u) => ChildrensCategory(u));
        }
        public Category Unproductive()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Unproductive)
                .RuleFor(u => u.Childrens, (_, u) => ChildrensCategory(u));
        }
        public Category Neutral()
        {
            return new Faker<Category>()
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Neutral)
                .RuleFor(u => u.Childrens, (_, u) => ChildrensCategory(u));
        }

        private ObservableCollection<Category> ChildrensCategory(Category category)
        {
            var list = new ObservableCollection<Category>();

            var amount = RandomNumberGenerator.GetInt32(1, 7);

            for(var index = 0; index < amount; index++)
            {
                list.Add(new Faker<Category>()
                .RuleFor(u => u.Id, () => index+1)
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => category.Type));
            }

            return list;
        }
    }
}
