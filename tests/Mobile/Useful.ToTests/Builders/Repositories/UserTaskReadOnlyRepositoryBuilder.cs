using Bogus;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Timerom.App.Repository.Interface;
using Timerom.App.ValueObjects.Entity;
using Useful.ToTests.Builders.Entity;

namespace Useful.ToTests.Builders.Repositories
{
    public class UserTaskReadOnlyRepositoryBuilder
    {
        private static UserTaskReadOnlyRepositoryBuilder _instance;
        private readonly Mock<IUserTaskReadOnlyRepository> _repository;

        private UserTaskReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IUserTaskReadOnlyRepository>();
        }

        public static UserTaskReadOnlyRepositoryBuilder Instance()
        {
            _instance = new UserTaskReadOnlyRepositoryBuilder();
            return _instance;
        }

        public UserTaskReadOnlyRepositoryBuilder ExistTaskForSubcategory()
        {
            _repository.Setup(c => c.ExistTaskForSubcategory(It.IsAny<long>())).ReturnsAsync(true);
            return this;
        }

        public UserTaskReadOnlyRepositoryBuilder GetAll_Empty()
        {
            _repository.Setup(c => c.GetAll(It.IsAny<DateTime>())).ReturnsAsync(new List<UserTask>());
            return this;
        }

        public UserTaskReadOnlyRepositoryBuilder GetAll()
        {
            var response = new List<UserTask>();

            (_, IList<Category> childrens) = CategoryEntityBuilder.Instance().Productive();

            response.Add(new Faker<UserTask>()
                .RuleFor(u => u.Id, () => 1)
                .RuleFor(u => u.Title, (f) => f.Internet.UserName())
                .RuleFor(u => u.Description, (f) => f.Lorem.Paragraph())
                .RuleFor(u => u.StartsAt, () => DateTime.Today.Date)
                .RuleFor(u => u.EndsAt, (_, u) => u.StartsAt.AddMinutes(60))
                .RuleFor(u => u.CategoryId, () => childrens.First().Id));

            (_, childrens) = CategoryEntityBuilder.Instance().Neutral();

            response.Add(new Faker<UserTask>()
                .RuleFor(u => u.Id, () => 2)
                .RuleFor(u => u.Title, (f) => f.Internet.UserName())
                .RuleFor(u => u.Description, (f) => f.Lorem.Paragraph())
                .RuleFor(u => u.StartsAt, () => DateTime.Today.Date.AddMinutes(60))
                .RuleFor(u => u.EndsAt, (_, u) => u.StartsAt.AddMinutes(60))
                .RuleFor(u => u.CategoryId, () => childrens.First().Id));

            (_, childrens) = CategoryEntityBuilder.Instance().Unproductive();

            response.Add(new Faker<UserTask>()
                .RuleFor(u => u.Id, () => 3)
                .RuleFor(u => u.Title, (f) => f.Internet.UserName())
                .RuleFor(u => u.Description, (f) => f.Lorem.Paragraph())
                .RuleFor(u => u.StartsAt, () => DateTime.Today.Date.AddMinutes(120))
                .RuleFor(u => u.EndsAt, (_, u) => u.StartsAt.AddMinutes(60))
                .RuleFor(u => u.CategoryId, () => childrens.First().Id));

            _repository.Setup(c => c.GetAll(It.IsAny<DateTime>())).ReturnsAsync(response);
            return this;
        }

        public UserTaskReadOnlyRepositoryBuilder GetById(Timerom.App.Model.TaskModel task)
        {
            _repository.Setup(c => c.GetById(task.Id)).ReturnsAsync(new UserTask
            {
                Id = task.Id,
                Description = task.Description,
                Title = task.Title,
                EndsAt = task.EndsAt,
                StartsAt = task.StartsAt,
                CategoryId = task.Category.Id
            });

            return this;
        }

        public IUserTaskReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
