using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.UserTask.Local.Delete;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace UseCases.Test.UserTask.Local.Delete
{
    public class DeleteUserTaskUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var request = RequestTask.Instance().Build();

            var repositoryUserTaskRead = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetById(request).Build());
            var repositoryUserTaskWrite = new Lazy<IUserTaskWriteOnlyRepository>(() => UserTaskWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new DeleteUserTaskUseCase(repositoryUserTaskRead, repositoryUserTaskWrite);

            Func<Task> action = async () => await useCase.Execute(request);

            await action.Should().NotThrowAsync();
        }
    }
}
