using Moq;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class UpdateCategoryUseCaseBuilder
    {
        private static UpdateCategoryUseCaseBuilder _instance;
        private readonly Mock<IUpdateCategoryUseCase> _repository;

        private UpdateCategoryUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IUpdateCategoryUseCase>();
        }

        public static UpdateCategoryUseCaseBuilder Instance()
        {
            _instance = new UpdateCategoryUseCaseBuilder();
            return _instance;
        }

        public IUpdateCategoryUseCase Build()
        {
            return _repository.Object;
        }
    }
}
