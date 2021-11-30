using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Local.Update;
using Timerom.App.ValueObjects.Entity;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace UseCases.Test.Categories.Local.Update
{
    public class UpdateCategoryUseCaseTest
    {
        [Fact]
        public async Task Category_Sucess()
        {
            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Productive();

            var repositoryRead = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById(parent, childrens).Build());
            var repositoryWrite = new Lazy<ICategoryWriteOnlyRepository>(() => CategoryWriteOnlyRepositoryBuilder.Instance().Build());
            var repositoryUserTask = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().Build());
            
            var useCase = new UpdateCategoryUseCase(repositoryWrite, repositoryRead, repositoryUserTask);

            var firstSubcategory = childrens.First();
            var request = RequestCategory.Instance().Productive();
            request.Id = parent.Id;
            request.Childrens.Add(new Timerom.App.Model.Category
            {
                Name = firstSubcategory.Name,
                Type = firstSubcategory.Type
            });

            Timerom.App.Model.Category response = null;
            Func<Task> action = async () => response = await useCase.Execute(request);

            await action.Should().NotThrowAsync();

            response.Name.Should().Be(request.Name);
        }

        [Fact]
        public async Task Category_Empty_Name()
        {
            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Productive();

            var repositoryRead = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById(parent, childrens).Build());
            var repositoryWrite = new Lazy<ICategoryWriteOnlyRepository>(() => CategoryWriteOnlyRepositoryBuilder.Instance().Build());
            var repositoryUserTask = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().Build());

            var useCase = new UpdateCategoryUseCase(repositoryWrite, repositoryRead, repositoryUserTask);

            var request = RequestCategory.Instance().Productive();
            request.Name = "";

            Func<Task> action = async () => await useCase.Execute(request);

            await action.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(c => c.ErrorMensages.Count == 1 && c.ErrorMensages.Contains(ResourceTextException.NAME_IS_REQUIRED));
        }

        [Fact]
        public async Task Category_Cant_Delete_Subcategory()
        {
            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Productive();

            var repositoryRead = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById(parent, childrens).Build());
            var repositoryWrite = new Lazy<ICategoryWriteOnlyRepository>(() => CategoryWriteOnlyRepositoryBuilder.Instance().Build());
            var repositoryUserTask = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().ExistTaskForSubcategory().Build());

            var useCase = new UpdateCategoryUseCase(repositoryWrite, repositoryRead, repositoryUserTask);

            var request = RequestCategory.Instance().Productive();
            request.Id = parent.Id;

            Func<Task> action = async () => await useCase.Execute(request);

            await action.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(c => c.ErrorMensages.Count == 1 && c.ErrorMensages.Contains(ResourceTextException.SOME_SUBCATEGORIES_CANNOT_REMOVED_ASSOCIATED_TASKS));
        }
    }
}
