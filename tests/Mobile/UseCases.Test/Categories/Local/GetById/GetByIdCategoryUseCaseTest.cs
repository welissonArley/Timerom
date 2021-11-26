using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Local.GetById;
using Timerom.App.ValueObjects.Entity;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Categories.Local.GetById
{
    public class GetByIdCategoryUseCaseTest
    {
        [Fact]
        public async Task Category_Sucess()
        {
            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Build();

            var repository = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById(parent, childrens).Build());

            var useCase = new GetByIdCategoryUseCase(repository);

            Timerom.App.Model.Category category = null;

            Func<Task> action = async () => category = await useCase.Execute(parent.Id);

            await action.Should().NotThrowAsync();

            category.Should().NotBeNull();
            category.Id.Should().Be(parent.Id);
            category.Name.Should().Be(parent.Name);
            category.Type.Should().Be(parent.Type);
            category.Childrens.Should().HaveCount(childrens.Count);
            category.Parent.Should().BeNull();
        }

        [Fact]
        public async Task Subcategory_Sucess()
        {
            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Build();

            var repository = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetById(parent, childrens).Build());

            var useCase = new GetByIdCategoryUseCase(repository);

            Timerom.App.Model.Category category = null;

            foreach(var subcategory in childrens)
            {
                Func<Task> action = async () => category = await useCase.Execute(subcategory.Id);

                await action.Should().NotThrowAsync();

                category.Should().NotBeNull();
                category.Id.Should().Be(subcategory.Id);
                category.Name.Should().Be(subcategory.Name);
                category.Type.Should().Be(subcategory.Type);
                category.Childrens.Should().BeEmpty();
                category.Parent.Should().NotBeNull();
                category.Parent.Id.Should().Be(parent.Id);
            }
        }
    }
}
