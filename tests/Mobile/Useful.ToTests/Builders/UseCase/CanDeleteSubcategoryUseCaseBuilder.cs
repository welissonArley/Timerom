using Moq;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class CanDeleteSubcategoryUseCaseBuilder
    {
        private static CanDeleteSubcategoryUseCaseBuilder _instance;
        private readonly Mock<ICanDeleteSubcategoryUseCase> _repository;

        private CanDeleteSubcategoryUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<ICanDeleteSubcategoryUseCase>();
        }

        public static CanDeleteSubcategoryUseCaseBuilder Instance()
        {
            _instance = new CanDeleteSubcategoryUseCaseBuilder();
            return _instance;
        }

        public ICanDeleteSubcategoryUseCase Build()
        {
            return _repository.Object;
        }
    }
}
