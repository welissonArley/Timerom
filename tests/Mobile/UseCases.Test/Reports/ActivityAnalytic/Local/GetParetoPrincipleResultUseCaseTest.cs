using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Local;
using Timerom.App.ValueObjects.Dto;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Reports.ActivityAnalytic.Local
{
    public class GetParetoPrincipleResultUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var categoryBuilder = CategoryReadOnlyRepositoryBuilder.Instance();
            var repository = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetBetweenDates(categoryBuilder).Build());
            var categoryRepository = new Lazy<ICategoryReadOnlyRepository>(() => categoryBuilder.Build());

            var useCase = new GetParetoPrincipleResultUseCase(categoryRepository, repository);

            ParetoPrincipleModel response = null;
            Func<Task> action = async () => response = await useCase.Execute(new ParetoPrincipleFilter());

            await action.Should().NotThrowAsync();

            response.Ranking.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task Sucess_Empty()
        {
            var repository = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetBetweenDates_Empty().Build());
            var categoryRepository = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().Build());

            var useCase = new GetParetoPrincipleResultUseCase(categoryRepository, repository);

            ParetoPrincipleModel response = null;
            Func<Task> action = async () => response = await useCase.Execute(new ParetoPrincipleFilter());

            await action.Should().NotThrowAsync();

            response.Ranking.Should().BeEmpty();
        }

        [Theory]
        [ClassData(typeof(ParetoPrincipleInlineDataTest))]
        public async Task Sucess_Filter(ParetoPrincipleFilter filter)
        {
            var categoryBuilder = CategoryReadOnlyRepositoryBuilder.Instance();
            var repository = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetBetweenDates(categoryBuilder).Build());
            var categoryRepository = new Lazy<ICategoryReadOnlyRepository>(() => categoryBuilder.Build());

            var useCase = new GetParetoPrincipleResultUseCase(categoryRepository, repository);
            
            ParetoPrincipleModel response = null;
            Func<Task> action = async () => response = await useCase.Execute(filter);

            await action.Should().NotThrowAsync();

            response.Ranking.Should().HaveCountGreaterThan(0);
        }
    }
}
