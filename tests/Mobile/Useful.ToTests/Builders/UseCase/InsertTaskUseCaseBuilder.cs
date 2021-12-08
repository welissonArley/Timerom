using Moq;
using Timerom.App.UseCase.UserTask.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class InsertTaskUseCaseBuilder
    {
        private static InsertTaskUseCaseBuilder _instance;
        private readonly Mock<IInsertTaskUseCase> _repository;

        private InsertTaskUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IInsertTaskUseCase>();
        }

        public static InsertTaskUseCaseBuilder Instance()
        {
            _instance = new InsertTaskUseCaseBuilder();
            return _instance;
        }

        public IInsertTaskUseCase Build()
        {
            return _repository.Object;
        }
    }
}
