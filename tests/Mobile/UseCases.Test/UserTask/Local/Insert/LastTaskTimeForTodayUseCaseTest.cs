using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.UserTask.Local.Insert;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.UserTask.Local.Insert
{
    public class LastTaskTimeForTodayUseCaseTest
    {
        [Fact]
        public async Task Sucess_NoTasks()
        {
            var repositoryUserTaskRead = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().Build());

            var useCase = new LastTaskTimeUseCase(repositoryUserTaskRead);

            DateTime response = new DateTime();
            Func<Task> action = async () => response = await useCase.Execute();

            await action.Should().NotThrowAsync();

            response.Date.Should().Be(DateTime.Today);
        }

        [Fact]
        public async Task Sucess_WithTasks()
        {
            DateTime lastTask = DateTime.Now.AddHours(-2);
            var repositoryUserTaskRead = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetLast(lastTask).Build());

            var useCase = new LastTaskTimeUseCase(repositoryUserTaskRead);

            DateTime response = new DateTime();
            Func<Task> action = async () => response = await useCase.Execute();

            await action.Should().NotThrowAsync();

            response.Should().Be(lastTask);
        }
    }
}
