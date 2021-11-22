using Moq;
using Timerom.App.Repository.Interface;

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

        public IUserTaskReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
