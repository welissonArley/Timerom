using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.UserTask.Local.Insert;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace UseCases.Test.UserTask.Local.Insert
{
    public class InsertTaskUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var repositoryWrite = new Lazy<IUserTaskWriteOnlyRepository>(() => UserTaskWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new InsertTaskUseCase(repositoryWrite);

            var request = RequestTask.Instance().Build();

            Func<Task> action = async () => await useCase.Execute(request);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Empty_Title()
        {
            var repositoryWrite = new Lazy<IUserTaskWriteOnlyRepository>(() => UserTaskWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new InsertTaskUseCase(repositoryWrite);

            var request = RequestTask.Instance().Build();
            request.Title = "";

            Func<Task> action = async () => await useCase.Execute(request);

            await action.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(c => c.ErrorMensages.Count == 1 && c.ErrorMensages.Contains(ResourceTextException.TASK_TITLE_REQUIRED));
        }
    }
}
