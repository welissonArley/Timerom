using Moq;
using System;
using System.Collections.ObjectModel;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Useful.ToTests.Builders.Request;

namespace Useful.ToTests.Builders.UseCase
{
    public class ActivityAnalyticDetailsSubCategoryUseCaseBuilder
    {
        private static ActivityAnalyticDetailsSubCategoryUseCaseBuilder _instance;
        private readonly Mock<IActivityAnalyticDetailsSubCategoryUseCase> _repository;

        private ActivityAnalyticDetailsSubCategoryUseCaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IActivityAnalyticDetailsSubCategoryUseCase>();
        }

        public static ActivityAnalyticDetailsSubCategoryUseCaseBuilder Instance()
        {
            _instance = new ActivityAnalyticDetailsSubCategoryUseCaseBuilder();
            return _instance;
        }

        public ActivityAnalyticDetailsSubCategoryUseCaseBuilder Execute()
        {
            _repository.Setup(c => c.Execute(It.IsAny<long>(), It.IsAny<DateTime>())).ReturnsAsync(new ObservableCollection<Timerom.App.Model.ActivitiesAnalyticModel>
            {
                RequestActivitiesAnalyticModel.Instance().Build()
            });
            return this;
        }

        public IActivityAnalyticDetailsSubCategoryUseCase Build()
        {
            return _repository.Object;
        }
    }
}
