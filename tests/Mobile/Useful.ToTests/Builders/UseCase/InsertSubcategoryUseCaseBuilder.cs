using Moq;
using Timerom.App.Model;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Useful.ToTests.Builders.UseCase
{
    public class InsertSubcategoryUseCaseBuilder
    {
        private static InsertSubcategoryUseCaseBuilder _instance;
        private readonly Mock<IInsertSubcategoryUseCase> _repository;

        private InsertSubcategoryUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IInsertSubcategoryUseCase>();
        }

        public static InsertSubcategoryUseCaseBuilder Instance()
        {
            _instance = new InsertSubcategoryUseCaseBuilder();
            return _instance;
        }

        public InsertSubcategoryUseCaseBuilder Execute(Category category)
        {
            _repository.Setup(x => x.Execute(It.IsAny<Category>(), It.IsAny<long>())).ReturnsAsync(category);
            return this;
        }

        public IInsertSubcategoryUseCase Build()
        {
            return _repository.Object;
        }
    }
}
