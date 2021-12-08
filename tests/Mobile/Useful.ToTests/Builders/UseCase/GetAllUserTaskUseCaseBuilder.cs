using Moq;
using System;
using System.Collections.Generic;
using Timerom.App.Model;
using Timerom.App.UseCase.UserTask.Interfaces;
using Useful.ToTests.Builders.Request;

namespace Useful.ToTests.Builders.UseCase
{
    public class GetAllUserTaskUseCaseBuilder
    {
        private static GetAllUserTaskUseCaseBuilder _instance;
        private readonly Mock<IGetAllUserTaskUseCase> _repository;

        private GetAllUserTaskUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IGetAllUserTaskUseCase>();
        }

        public static GetAllUserTaskUseCaseBuilder Instance()
        {
            _instance = new GetAllUserTaskUseCaseBuilder();
            return _instance;
        }

        public GetAllUserTaskUseCaseBuilder Execute()
        {
            _repository.Setup(c => c.Execute(It.IsAny<DateTime>(), It.IsAny<IList<long>>())).ReturnsAsync(RequestTasksDetailsModel.Instance().Build());
            return this;
        }

        public IGetAllUserTaskUseCase Build()
        {
            return _repository.Object;
        }
    }
}
