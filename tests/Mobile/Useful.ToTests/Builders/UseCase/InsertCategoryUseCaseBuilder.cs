using Moq;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class InsertCategoryUseCaseBuilder
    {
        private static InsertCategoryUseCaseBuilder _instance;
        private readonly Mock<IInsertCategoryUseCase> _repository;

        private InsertCategoryUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IInsertCategoryUseCase>();
        }

        public static InsertCategoryUseCaseBuilder Instance()
        {
            _instance = new InsertCategoryUseCaseBuilder();
            return _instance;
        }

        public IInsertCategoryUseCase Build()
        {
            return _repository.Object;
        }
    }
}
