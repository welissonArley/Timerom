using Moq;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class UpdateSubcategoryUseCaseBuilder
    {
        private static UpdateSubcategoryUseCaseBuilder _instance;
        private readonly Mock<IUpdateSubcategoryUseCase> _repository;

        private UpdateSubcategoryUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IUpdateSubcategoryUseCase>();
        }

        public static UpdateSubcategoryUseCaseBuilder Instance()
        {
            _instance = new UpdateSubcategoryUseCaseBuilder();
            return _instance;
        }

        public IUpdateSubcategoryUseCase Build()
        {
            return _repository.Object;
        }
    }
}
