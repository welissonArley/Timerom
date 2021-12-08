using Moq;
using Timerom.App.UseCase.UserTask.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class DeleteUserTaskUseCaseBuilder
    {
        private static DeleteUserTaskUseCaseBuilder _instance;
        private readonly Mock<IDeleteUserTaskUseCase> _repository;

        private DeleteUserTaskUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IDeleteUserTaskUseCase>();
        }

        public static DeleteUserTaskUseCaseBuilder Instance()
        {
            _instance = new DeleteUserTaskUseCaseBuilder();
            return _instance;
        }

        public IDeleteUserTaskUseCase Build()
        {
            return _repository.Object;
        }
    }
}
