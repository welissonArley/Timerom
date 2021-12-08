using Moq;
using Timerom.App.Repository.Interface;

namespace Useful.ToTests.Builders.Repositories
{
    public class UserTaskWriteOnlyRepositoryBuilder
    {
        private static UserTaskWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<IUserTaskWriteOnlyRepository> _repository;

        private UserTaskWriteOnlyRepositoryBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IUserTaskWriteOnlyRepository>();
        }

        public static UserTaskWriteOnlyRepositoryBuilder Instance()
        {
            _instance = new UserTaskWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public IUserTaskWriteOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
