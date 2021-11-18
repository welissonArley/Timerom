using Moq;
using Timerom.App.UseCase.UserTask.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class UpdateUserTaskUseCase
    {
        private static UpdateUserTaskUseCase _instance;
        private readonly Mock<IUpdateUserTaskUseCase> _repository;

        private UpdateUserTaskUseCase()
        {
            if (_repository == null)
                _repository = new Mock<IUpdateUserTaskUseCase>();
        }

        public static UpdateUserTaskUseCase Instance()
        {
            _instance = new UpdateUserTaskUseCase();
            return _instance;
        }

        public IUpdateUserTaskUseCase Build()
        {
            return _repository.Object;
        }
    }
}
