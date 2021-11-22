using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Local.Delete;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace UseCases.Test.Categories.Local.Delete
{
    public class DeleteCategoryUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var repositoryUserTaskRead = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().Build());
            var repositoryCategoryRead = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById().GetChildrensByParentId().Build());
            var repositoryCategoryWrite = new Lazy<ICategoryWriteOnlyRepository>(() => CategoryWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new DeleteCategoryUseCase(repositoryCategoryWrite, repositoryCategoryRead, repositoryUserTaskRead);

            var category = RequestCategory.Instance().Build();
            Func<Task> action = async () => await useCase.Execute(category);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Cannot_Delete()
        {
            var repositoryUserTaskRead = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().ExistTaskForSubcategory().Build());
            var repositoryCategoryRead = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById().GetChildrensByParentId().Build());
            var repositoryCategoryWrite = new Lazy<ICategoryWriteOnlyRepository>(() => CategoryWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new DeleteCategoryUseCase(repositoryCategoryWrite, repositoryCategoryRead, repositoryUserTaskRead);

            var category = RequestCategory.Instance().Build();
            Func<Task> action = async () => await useCase.Execute(category);

            await action.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(c => c.ErrorMensages.Count == 1 && c.ErrorMensages.Contains(ResourceTextException.CATEGORY_CONTAIS_SUBCATEGORIES_ASSOCIATED_TASKS));
        }
    }
}
