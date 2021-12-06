using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Local;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Reports.ActivityAnalytic.Local
{
    public class ActivityAnalyticDetailsUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var categoryBuilder = CategoryReadOnlyRepositoryBuilder.Instance();
            var repository = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetBetweenDates(categoryBuilder).Build());
            var categoryRepository = new Lazy<ICategoryReadOnlyRepository>(() => categoryBuilder.Build());

            var useCase = new ActivityAnalyticDetailsUseCase(categoryRepository, repository);

            ActivityAnalyticStatisticsModel response = null;
            Func<Task> action = async () => response = await useCase.Execute(DateTime.Now);

            await action.Should().NotThrowAsync();

            response.Activities.Should().HaveCountGreaterThan(0);
        }
    }
}
