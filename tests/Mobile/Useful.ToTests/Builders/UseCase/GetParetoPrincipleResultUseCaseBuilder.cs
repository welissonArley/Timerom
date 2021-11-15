using Moq;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Dto;

namespace Useful.ToTests.Builders.UseCase
{
    public class GetParetoPrincipleResultUseCaseBuilder
    {
        private static GetParetoPrincipleResultUseCaseBuilder _instance;
        private readonly Mock<IGetParetoPrincipleResultUseCase> _repository;

        private GetParetoPrincipleResultUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IGetParetoPrincipleResultUseCase>();
        }

        public static GetParetoPrincipleResultUseCaseBuilder Instance()
        {
            _instance = new GetParetoPrincipleResultUseCaseBuilder();
            return _instance;
        }

        public GetParetoPrincipleResultUseCaseBuilder Execute()
        {
            _repository.Setup(c => c.Execute(It.IsAny<ParetoPrincipleFilter>())).ReturnsAsync(new ParetoPrincipleModel());
            return this;
        }

        public IGetParetoPrincipleResultUseCase Build()
        {
            return _repository.Object;
        }
    }
}
