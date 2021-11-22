using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Local.GetById;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Categories.Local.GetById
{
    public class GetByIdCategoryUseCaseTest
    {
        [Fact]
        public async Task Category_Sucess()
        {
            var repository = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById().GetChildrensByParentId().Build());

            var useCase = new GetByIdCategoryUseCase(repository);

            Timerom.App.Model.Category category = null;

            Func<Task> action = async () => category = await useCase.Execute(1);

            await action.Should().NotThrowAsync();

            category.Should().NotBeNull();
        }

        [Fact]
        public async Task Subcategory_Sucess()
        {
            var repository = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById_Subcategory().Build());

            var useCase = new GetByIdCategoryUseCase(repository);

            Timerom.App.Model.Category category = null;

            Func<Task> action = async () => category = await useCase.Execute(1);

            await action.Should().NotThrowAsync();

            category.Should().NotBeNull();
        }
    }
}
