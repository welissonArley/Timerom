using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Local.Delete;
using Timerom.App.ValueObjects.Entity;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;
using Useful.ToTests.Builders.Entity;
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
            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Build();

            var repositoryUserTaskRead = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().Build());
            var repositoryCategoryRead = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById(parent, childrens).Build());
            var repositoryCategoryWrite = new Lazy<ICategoryWriteOnlyRepository>(() => CategoryWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new DeleteCategoryUseCase(repositoryCategoryWrite, repositoryCategoryRead, repositoryUserTaskRead);

            var category = RequestCategory.Instance().Build();
            category.Id = parent.Id;

            Func<Task> action = async () => await useCase.Execute(category);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Cannot_Delete()
        {
            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Build();

            var repositoryUserTaskRead = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().ExistTaskForSubcategory().Build());
            var repositoryCategoryRead = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById(parent, childrens).Build());
            var repositoryCategoryWrite = new Lazy<ICategoryWriteOnlyRepository>(() => CategoryWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new DeleteCategoryUseCase(repositoryCategoryWrite, repositoryCategoryRead, repositoryUserTaskRead);

            var category = RequestCategory.Instance().Build();
            category.Id = parent.Id;

            Func<Task> action = async () => await useCase.Execute(category);

            await action.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(c => c.ErrorMensages.Count == 1 && c.ErrorMensages.Contains(ResourceTextException.CATEGORY_CONTAIS_SUBCATEGORIES_ASSOCIATED_TASKS));
        }
    }
}
