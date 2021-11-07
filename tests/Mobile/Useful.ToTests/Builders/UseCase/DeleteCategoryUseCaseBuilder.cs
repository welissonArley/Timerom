using Moq;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class DeleteCategoryUseCaseBuilder
    {
        private static DeleteCategoryUseCaseBuilder _instance;
        private readonly Mock<IDeleteCategoryUseCase> _repository;

        private DeleteCategoryUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IDeleteCategoryUseCase>();
        }

        public static DeleteCategoryUseCaseBuilder Instance()
        {
            _instance = new DeleteCategoryUseCaseBuilder();
            return _instance;
        }

        public IDeleteCategoryUseCase Build()
        {
            return _repository.Object;
        }
    }
}
