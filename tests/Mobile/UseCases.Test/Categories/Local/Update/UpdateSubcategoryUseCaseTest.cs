using FluentAssertions;
using System;
using System.Collections.Generic;
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
    public class UpdateSubcategoryUseCaseTest
    {
        [Fact]
        public async Task Category_Sucess()
        {
            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Build();

            var repositoryRead = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById(parent, childrens).Build());
            var repositoryWrite = new Lazy<ICategoryWriteOnlyRepository>(() => CategoryWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new UpdateSubcategoryUseCase(repositoryWrite, repositoryRead);

            foreach(var subcategory in childrens)
            {
                var request = RequestSubcategory.Instance().Build();
                request.Id = subcategory.Id;

                Func<Task> action = async () => await useCase.Execute(request, subcategory.ParentCategoryId.Value);

                await action.Should().NotThrowAsync();

                subcategory.Name.Should().Be(request.Name);
                subcategory.Type.Should().Be(subcategory.Type);
            }
        }

        [Fact]
        public async Task Category_Empty_Name()
        {
            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Build();

            var repositoryRead = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById(parent, childrens).Build());
            var repositoryWrite = new Lazy<ICategoryWriteOnlyRepository>(() => CategoryWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new UpdateSubcategoryUseCase(repositoryWrite, repositoryRead);

            foreach (var subcategory in childrens)
            {
                var request = RequestSubcategory.Instance().Build();
                request.Name = "";
                request.Id = subcategory.Id;

                Func<Task> action = async () => await useCase.Execute(request, subcategory.ParentCategoryId.Value);

                await action.Should().ThrowAsync<ErrorOnValidationException>()
                    .Where(c => c.ErrorMensages.Count == 1 && c.ErrorMensages.Contains(ResourceTextException.NAME_IS_REQUIRED));
            }
        }
    }
}
