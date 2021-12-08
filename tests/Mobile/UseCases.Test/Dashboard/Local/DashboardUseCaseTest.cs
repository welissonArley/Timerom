using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Dashboard.Local;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Dashboard.Local
{
    public class DashboardUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var repository = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetAll().Build());
            var categoryRepository = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetAll().Build());

            var useCase = new DashboardUseCase(categoryRepository, repository);

            DashboardModel response = null;
            Func<Task> action = async () => response = await useCase.Execute(DateTime.Now);

            await action.Should().NotThrowAsync();

            response.TotalTasks.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Sucess_Empty()
        {
            var repository = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetAll_Empty().Build());
            var categoryRepository = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().Build());

            var useCase = new DashboardUseCase(categoryRepository, repository);

            DashboardModel response = null;
            Func<Task> action = async () => response = await useCase.Execute(DateTime.Now);

            await action.Should().NotThrowAsync();

            response.TotalTasks.Should().Be(0);
        }
    }
}
