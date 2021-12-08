using Moq;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class GetByIdCategoryUseCaseBuilder
    {
        private static GetByIdCategoryUseCaseBuilder _instance;
        private readonly Mock<IGetByIdCategoryUseCase> _repository;

        private GetByIdCategoryUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IGetByIdCategoryUseCase>();
        }

        public static GetByIdCategoryUseCaseBuilder Instance()
        {
            _instance = new GetByIdCategoryUseCaseBuilder();
            return _instance;
        }

        public IGetByIdCategoryUseCase Build()
        {
            return _repository.Object;
        }
    }
}
