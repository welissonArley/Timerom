using Moq;
using System.Collections.Generic;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Useful.ToTests.Builders.Request;

namespace Useful.ToTests.Builders.UseCase
{
    public class GetChartActivityAnalyticUseCaseBuilder
    {
        private static GetChartActivityAnalyticUseCaseBuilder _instance;
        private readonly Mock<IGetChartActivityAnalyticUseCase> _repository;

        private GetChartActivityAnalyticUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IGetChartActivityAnalyticUseCase>();
        }

        public static GetChartActivityAnalyticUseCaseBuilder Instance()
        {
            _instance = new GetChartActivityAnalyticUseCaseBuilder();
            return _instance;
        }

        public GetChartActivityAnalyticUseCaseBuilder Execute()
        {
            _repository.Setup(c => c.Execute()).ReturnsAsync(new List<ChartActivityAnalyticModel>
            {
                RequestChartActivityAnalyticModel.Instance().Build()
            });
            return this;
        }

        public GetChartActivityAnalyticUseCaseBuilder Empty()
        {
            _repository.Setup(c => c.Execute()).ReturnsAsync(new List<ChartActivityAnalyticModel>());
            return this;
        }

        public IGetChartActivityAnalyticUseCase Build()
        {
            return _repository.Object;
        }
    }
}
