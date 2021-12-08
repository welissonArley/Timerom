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
    public class GetActivityAnalyticTotalUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var categoryBuilder = CategoryReadOnlyRepositoryBuilder.Instance();
            var repository = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetBetweenDates(categoryBuilder).Build());
            var categoryRepository = new Lazy<ICategoryReadOnlyRepository>(() => categoryBuilder.Build());

            var useCase = new GetActivityAnalyticTotalUseCase(categoryRepository, repository);

            ActivityAnalyticModel response = null;
            Func<Task> action = async () => response = await useCase.Execute();

            await action.Should().NotThrowAsync();

            response.Total.Should().NotBeNull();
            response.Productive.Should().NotBeNull();
            response.Neutral.Should().NotBeNull();
            response.Unproductive.Should().NotBeNull();
        }
    }
}
