using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.UserTask.Local.Update;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace UseCases.Test.UserTask.Local.Update
{
    public class UpdateUserTaskUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var request = RequestTask.Instance().Build();

            var repositoryUserTaskRead = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetById(request).Build());
            var repositoryUserTaskWrite = new Lazy<IUserTaskWriteOnlyRepository>(() => UserTaskWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new UpdateUserTaskUseCase(repositoryUserTaskWrite, repositoryUserTaskRead);

            Func<Task> action = async () => await useCase.Execute(request);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Empty_Title()
        {
            var request = RequestTask.Instance().Build();

            var repositoryUserTaskRead = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetById(request).Build());
            var repositoryUserTaskWrite = new Lazy<IUserTaskWriteOnlyRepository>(() => UserTaskWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new UpdateUserTaskUseCase(repositoryUserTaskWrite, repositoryUserTaskRead);

            request.Title = "";
            Func<Task> action = async () => await useCase.Execute(request);

            await action.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(c => c.ErrorMensages.Count == 1 && c.ErrorMensages.Contains(ResourceTextException.TASK_TITLE_REQUIRED));
        }
    }
}
