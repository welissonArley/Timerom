using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Model;
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

            TasksDetailsModel response = null;
            Func<Task> action = async () => response = await useCase.Execute(DateTime.Now, new List<long>());

            await action.Should().NotThrowAsync();

            response.Tasks.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task Sucess_Filter()
        {
            var categoryRepositoryInstance = CategoryReadOnlyRepositoryBuilder.Instance();
            var repositoryUserTaskRead = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetAll(categoryRepositoryInstance).Build());
            var repositoryCategoryRead = new Lazy<ICategoryReadOnlyRepository>(() => categoryRepositoryInstance.Build());

            var useCase = new GetAllUserTaskUseCase(repositoryCategoryRead, repositoryUserTaskRead);

            TasksDetailsModel response = null;

            var categoryIdToFilter = 2;
            Func<Task> action = async () => response = await useCase.Execute(DateTime.Now, new List<long> { categoryIdToFilter });

            await action.Should().NotThrowAsync();

            response.Tasks.Should().HaveCount(1);
        }
    }
}
