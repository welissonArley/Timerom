using Moq;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class DeleteSubcategoryUseCaseBuilder
    {
        private static DeleteSubcategoryUseCaseBuilder _instance;
        private readonly Mock<IDeleteSubcategoryUseCase> _repository;

        private DeleteSubcategoryUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IDeleteSubcategoryUseCase>();
        }

        public static DeleteSubcategoryUseCaseBuilder Instance()
        {
            _instance = new DeleteSubcategoryUseCaseBuilder();
            return _instance;
        }

        public IDeleteSubcategoryUseCase Build()
        {
            return _repository.Object;
        }
    }
}
