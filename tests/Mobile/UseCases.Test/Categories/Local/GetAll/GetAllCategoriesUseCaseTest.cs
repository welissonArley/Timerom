using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Local.GetAll;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Categories.Local.GetAll
{
    public class GetAllCategoriesUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var repository = new Lazy<ICategoryReadOnlyRepository>(() => CategoryReadOnlyRepositoryBuilder.Instance().GetAll().Build());

            var useCase = new GetAllCategoriesUseCase(repository);

            IList<Timerom.App.Model.Category> Productive = null;
            IList<Timerom.App.Model.Category> Neutral = null;
            IList<Timerom.App.Model.Category> Unproductive = null;

            Func<Task> action = async () => (Productive, Neutral, Unproductive) = await useCase.Execute();

            await action.Should().NotThrowAsync();

            Productive.Should().NotBeEmpty().And.OnlyContain(c => c.Type == Timerom.App.ValueObjects.Enuns.CategoryType.Productive);
            Neutral.Should().NotBeEmpty().And.OnlyContain(c => c.Type == Timerom.App.ValueObjects.Enuns.CategoryType.Neutral);
            Unproductive.Should().NotBeEmpty().And.OnlyContain(c => c.Type == Timerom.App.ValueObjects.Enuns.CategoryType.Unproductive);
        }
    }
}
