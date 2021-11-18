using Moq;
using System.Collections.Generic;
using Timerom.App.Model;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.Exception.ExceptionBase;

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

        public InsertCategoryUseCaseBuilder TimeromError()
        {
            _repository.Setup(c => c.Execute(It.IsAny<Category>())).Throws(new TimeromException(""));
            return this;
        }

        public InsertCategoryUseCaseBuilder ValidationError()
        {
            _repository.Setup(c => c.Execute(It.IsAny<Category>())).Throws(new ErrorOnValidationException(new List<string>()));
            return this;
        }

        public IInsertCategoryUseCase Build()
        {
            return _repository.Object;
        }
    }
}
