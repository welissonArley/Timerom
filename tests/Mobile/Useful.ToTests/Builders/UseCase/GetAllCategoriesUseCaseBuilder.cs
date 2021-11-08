using Moq;
using System.Collections.Generic;
using Timerom.App.UseCase.Categories.Interfaces;
using Useful.ToTests.Builders.Request;

namespace Useful.ToTests.Builders.UseCase
{
    public class GetAllCategoriesUseCaseBuilder
    {
        private static GetAllCategoriesUseCaseBuilder _instance;
        private readonly Mock<IGetAllCategoriesUseCase> _repository;

        private GetAllCategoriesUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IGetAllCategoriesUseCase>();
        }

        public static GetAllCategoriesUseCaseBuilder Instance()
        {
            _instance = new GetAllCategoriesUseCaseBuilder();
            return _instance;
        }

        public GetAllCategoriesUseCaseBuilder Categories()
        {
            var productive = RequestCategory.Instance().Productive();
            var neutral = RequestCategory.Instance().Neutral();
            var unproductive = RequestCategory.Instance().Unproductive();

            _repository.Setup(x => x.Execute()).ReturnsAsync((new List<Timerom.App.Model.Category> { productive },
                new List<Timerom.App.Model.Category> { neutral }, new List<Timerom.App.Model.Category> { unproductive }));

            return this;
        }

        public IGetAllCategoriesUseCase Build()
        {
            return _repository.Object;
        }
    }
}
