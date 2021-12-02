using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.UserTask.Local.GetAll;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.UserTask.Local.GetAll
{
    public class GetAllUserTaskUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var categoryRepositoryInstance = CategoryReadOnlyRepositoryBuilder.Instance();
            var repositoryUserTaskRead = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetAll(categoryRepositoryInstance).Build());
            var repositoryCategoryRead = new Lazy<ICategoryReadOnlyRepository>(() => categoryRepositoryInstance.Build());

            var useCase = new GetAllUserTaskUseCase(repositoryCategoryRead, repositoryUserTaskRead);

            Func<Task> action = async () => await useCase.Execute(DateTime.Now, new List<long>());

            await action.Should().NotThrowAsync();
        }
    }
}
