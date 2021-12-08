using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Local.Insert;
using Timerom.Exception;
using Timerom.Exception.ExceptionBase;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace UseCases.Test.Categories.Local.Insert
{
    public class InsertSubcategoryUseCaseTest
    {
        [Fact]
        public async Task Category_Sucess()
        {
            var repositoryRead = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().Build());
            var repositoryWrite = new Lazy<ICategoryWriteOnlyRepository>(() => CategoryWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new InsertSubcategoryUseCase(repositoryWrite, repositoryRead);

            Timerom.App.Model.Category category = null;

            var request = RequestSubcategory.Instance().Build();

            Func<Task> action = async () => category = await useCase.Execute(request, 1);

            await action.Should().NotThrowAsync();

            category.Should().NotBeNull();
            category.Name.Should().Be(request.Name);
            category.Type.Should().Be(request.Type);
            category.Childrens.Should().BeEmpty();
        }

        [Fact]
        public async Task Category_Empty_Name()
        {
            var repositoryRead = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().Build());
            var repositoryWrite = new Lazy<ICategoryWriteOnlyRepository>(() => CategoryWriteOnlyRepositoryBuilder.Instance().Build());

            var useCase = new InsertSubcategoryUseCase(repositoryWrite, repositoryRead);

            var request = RequestSubcategory.Instance().Build();
            request.Name = "";

            Func<Task> action = async () => await useCase.Execute(request, 1);

            await action.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(c => c.ErrorMensages.Count == 1 && c.ErrorMensages.Contains(ResourceTextException.NAME_IS_REQUIRED));
        }
    }
}
