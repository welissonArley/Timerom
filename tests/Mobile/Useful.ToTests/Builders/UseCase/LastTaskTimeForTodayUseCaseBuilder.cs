using Moq;
using Timerom.App.UseCase.UserTask.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class LastTaskTimeForTodayUseCaseBuilder
    {
        private static LastTaskTimeForTodayUseCaseBuilder _instance;
        private readonly Mock<ILastTaskTimeForTodayUseCase> _repository;

        private LastTaskTimeForTodayUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<ILastTaskTimeForTodayUseCase>();
        }

        public static LastTaskTimeForTodayUseCaseBuilder Instance()
        {
            _instance = new LastTaskTimeForTodayUseCaseBuilder();
            return _instance;
        }

        public ILastTaskTimeForTodayUseCase Build()
        {
            return _repository.Object;
        }
    }
}
