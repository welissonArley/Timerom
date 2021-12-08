using Moq;
using System;
using Timerom.App.UseCase.Dashboard.Interfaces;
using Useful.ToTests.Builders.Request;

namespace Useful.ToTests.Builders.UseCase
{
    public class DashboardUseCaseBuilder
    {
        private static DashboardUseCaseBuilder _instance;
        private readonly Mock<IDashboardUseCase> _repository;

        private DashboardUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IDashboardUseCase>();
        }

        public static DashboardUseCaseBuilder Instance()
        {
            _instance = new DashboardUseCaseBuilder();
            return _instance;
        }

        public DashboardUseCaseBuilder Execute()
        {
            _repository.Setup(c => c.Execute(It.IsAny<DateTime>())).ReturnsAsync(RequestDashboardModel.Instance().Build());
            return this;
        }

        public IDashboardUseCase Build()
        {
            return _repository.Object;
        }
    }
}
