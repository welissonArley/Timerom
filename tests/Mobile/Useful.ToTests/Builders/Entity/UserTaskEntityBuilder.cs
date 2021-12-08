using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using Timerom.App.ValueObjects.Entity;

namespace Useful.ToTests.Builders.Entity
{
    public class UserTaskEntityBuilder
    {
        private static UserTaskEntityBuilder _instance;

        public static UserTaskEntityBuilder Instance()
        {
            _instance = new UserTaskEntityBuilder();
            return _instance;
        }

        public (UserTask task, Category parent, IList<Category> childrens) Build()
        {
            var taskId = 1;

            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Build();

            return (CreateTask(taskId, childrens), parent, childrens);
        }

        public (UserTask task, Category parent, IList<Category> childrens) Productive()
        {
            var taskId = 2;

            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Productive();

            return (CreateTask(taskId, childrens), parent, childrens);
        }

        public (UserTask task, Category parent, IList<Category> childrens) Neutral()
        {
            var taskId = 3;

            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Neutral();

            return (CreateTask(taskId, childrens), parent, childrens);
        }

        public (UserTask task, Category parent, IList<Category> childrens) Unproductive()
        {
            var taskId = 4;

            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Unproductive();

            return (CreateTask(taskId, childrens), parent, childrens);
        }

        private UserTask CreateTask(long taskId, IList<Category> subcategories)
        {
            return new Faker<UserTask>()
                .RuleFor(u => u.Id, () => taskId)
                .RuleFor(u => u.Title, (f) => f.Internet.UserName())
                .RuleFor(u => u.Description, (f) => f.Lorem.Paragraph())
                .RuleFor(u => u.StartsAt, () => DateTime.Now.AddHours(-1))
                .RuleFor(u => u.EndsAt, () => DateTime.Now)
                .RuleFor(u => u.CategoryId, () => subcategories.First().Id);
        }
    }
}
