using Moq;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Useful.ToTests.Builders.Request;

namespace Useful.ToTests.Builders.UseCase
{
    public class GetActivityAnalyticTotalUseCaseBuilder
    {
        private static GetActivityAnalyticTotalUseCaseBuilder _instance;
        private readonly Mock<IGetActivityAnalyticTotalUseCase> _repository;

        private GetActivityAnalyticTotalUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IGetActivityAnalyticTotalUseCase>();
        }

        public static GetActivityAnalyticTotalUseCaseBuilder Instance()
        {
            _instance = new GetActivityAnalyticTotalUseCaseBuilder();
            return _instance;
        }

        public GetActivityAnalyticTotalUseCaseBuilder Execute()
        {
            _repository.Setup(c => c.Execute()).ReturnsAsync(RequestActivityAnalyticModel.Instance().Build());
            return this;
        }

        public IGetActivityAnalyticTotalUseCase Build()
        {
            return _repository.Object;
        }
    }
}
