using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Local;
using Timerom.App.ValueObjects.Enuns;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Reports.ActivityAnalytic.Local
{
    public class GetChartActivityAnalyticUseCaseTest
    {
        [Theory]
        [InlineData(CategoryType.Neutral)]
        [InlineData(CategoryType.Productive)]
        [InlineData(CategoryType.Unproductive)]
        public async Task Sucess(CategoryType categoryWithMaxTime)
        {
            var categoryBuilder = CategoryReadOnlyRepositoryBuilder.Instance();
            var repository = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetBetweenDates(categoryBuilder, categoryWithMaxTime).Build());
            var categoryRepository = new Lazy<ICategoryReadOnlyRepository>(() => categoryBuilder.Build());

            var useCase = new GetChartActivityAnalyticUseCase(categoryRepository, repository);

            IList<ChartActivityAnalyticModel> response = null;
            Func<Task> action = async () => response = await useCase.Execute();

            await action.Should().NotThrowAsync();

            response.Should().HaveCountGreaterThan(0);
        }
    }
}
