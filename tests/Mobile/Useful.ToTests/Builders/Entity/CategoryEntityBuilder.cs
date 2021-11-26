using Bogus;
using System.Collections.Generic;
using System.Security.Cryptography;
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

        public (Category parent, IList<Category> childrens) Build()
        {
            var parentId = 1;
            CategoryType type = CategoryType.Neutral;

            var parent = new Faker<Category>()
                .RuleFor(u => u.Id, () => parentId)
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, (f) => f.PickRandom<CategoryType>())
                .FinishWith((f, u) =>
                {
                    type = u.Type;
                });

            return (parent, CreateChildrens(type, parentId));
        }

        public (Category parent, IList<Category> childrens) Productive()
        {
            var parentId = 2;

            var parent = new Faker<Category>()
                .RuleFor(u => u.Id, () => parentId)
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Productive);

            return (parent, CreateChildrens(CategoryType.Productive, parentId));
        }
        public (Category parent, IList<Category> childrens) Unproductive()
        {
            var parentId = 3;

            var parent = new Faker<Category>()
                .RuleFor(u => u.Id, () => parentId)
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Unproductive);

            return (parent, CreateChildrens(CategoryType.Unproductive, parentId));
        }
        public (Category parent, IList<Category> childrens) Neutral()
        {
            var parentId = 4;

            var parent = new Faker<Category>()
                .RuleFor(u => u.Id, () => parentId)
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.Type, () => CategoryType.Neutral);

            return (parent, CreateChildrens(CategoryType.Neutral, parentId));
        }

        private IList<Category> CreateChildrens(CategoryType type, long parentId)
        {
            var childrens = new List<Category>();

            var amount = RandomNumberGenerator.GetInt32(1, 7);

            for (var index = 0; index < amount; index++)
            {
                childrens.Add(new Faker<Category>()
                .RuleFor(u => u.Id, () => index + 1 + (parentId * 100))
                .RuleFor(u => u.Name, (f) => f.Internet.UserName())
                .RuleFor(u => u.ParentCategoryId, () => parentId)
                .RuleFor(u => u.Type, () => type));
            }

            return childrens;
        }
    }
}
