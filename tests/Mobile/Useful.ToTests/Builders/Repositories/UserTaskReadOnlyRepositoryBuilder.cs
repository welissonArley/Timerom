using Moq;
using System;
using System.Collections.Generic;
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

        public UserTaskReadOnlyRepositoryBuilder GetAll(CategoryReadOnlyRepositoryBuilder categoryBuilder = null)
        {
            var response = GetUserTasks(categoryBuilder);

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

        public UserTaskReadOnlyRepositoryBuilder GetLast(DateTime endsAt)
        {
            _repository.Setup(c => c.GetLast(It.IsAny<DateTime>())).ReturnsAsync(new UserTask { EndsAt = endsAt});
            return this;
        }

        public UserTaskReadOnlyRepositoryBuilder GetBetweenDates(CategoryReadOnlyRepositoryBuilder categoryBuilder = null)
        {
            var response = GetUserTasks(categoryBuilder);

            _repository.Setup(c => c.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(response);
            return this;
        }

        private List<UserTask> GetUserTasks(CategoryReadOnlyRepositoryBuilder categoryBuilder = null)
        {
            var response = new List<UserTask>();

            (UserTask task, Category parent, IList<Category> childrens) = UserTaskEntityBuilder.Instance().Productive();
            categoryBuilder?.GetById(parent, childrens);
            response.Add(task);

            (task, parent, childrens) = UserTaskEntityBuilder.Instance().Neutral();
            categoryBuilder?.GetById(parent, childrens);
            response.Add(task);

            (task, parent, childrens) = UserTaskEntityBuilder.Instance().Unproductive();
            categoryBuilder?.GetById(parent, childrens);
            response.Add(task);

            return response;
        }

        public IUserTaskReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
