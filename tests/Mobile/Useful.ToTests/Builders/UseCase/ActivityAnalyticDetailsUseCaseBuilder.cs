using Moq;
using System;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Useful.ToTests.Builders.Request;

namespace Useful.ToTests.Builders.UseCase
{
    public class ActivityAnalyticDetailsUseCaseBuilder
    {
        private static ActivityAnalyticDetailsUseCaseBuilder _instance;
        private readonly Mock<IActivityAnalyticDetailsUseCase> _repository;

        private ActivityAnalyticDetailsUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IActivityAnalyticDetailsUseCase>();
        }

        public static ActivityAnalyticDetailsUseCaseBuilder Instance()
        {
            _instance = new ActivityAnalyticDetailsUseCaseBuilder();
            return _instance;
        }

        public ActivityAnalyticDetailsUseCaseBuilder Execute()
        {
            _repository.Setup(c => c.Execute(It.IsAny<DateTime>())).ReturnsAsync(RequestActivityAnalyticStatisticsModel.Instance().Build());
            return this;
        }

        public IActivityAnalyticDetailsUseCase Build()
        {
            return _repository.Object;
        }
    }
}
