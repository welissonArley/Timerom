using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Local;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Reports.ActivityAnalytic.Local
{
    public class ActivityAnalyticDetailsSubCategoryUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var categoryBuilder = CategoryReadOnlyRepositoryBuilder.Instance();
            var repository = new Lazy<IUserTaskReadOnlyRepository>(() => UserTaskReadOnlyRepositoryBuilder.Instance().GetBetweenDates(categoryBuilder).Build());
            var categoryRepository = new Lazy<ICategoryReadOnlyRepository>(() => categoryBuilder.Build());

            var useCase = new ActivityAnalyticDetailsSubCategoryUseCase(categoryRepository, repository);

            (_, _, IList<Timerom.App.ValueObjects.Entity.Category> childrens) = UserTaskEntityBuilder.Instance().Productive();
            ObservableCollection<ActivitiesAnalyticModel> response = null;
            Func<Task> action = async () => response = await useCase.Execute(childrens.First().ParentCategoryId.Value, DateTime.Now);

            await action.Should().NotThrowAsync();

            response.Should().HaveCountGreaterThan(0);
        }
    }
}
