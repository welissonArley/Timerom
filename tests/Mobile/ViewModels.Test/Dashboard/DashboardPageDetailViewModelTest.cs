using FluentAssertions;
using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Dashboard.Interfaces;
using Timerom.App.ViewModels.Dashboard;
using Useful.ToTests.Builders.Navigation;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCase;
using Xunit;

namespace ViewModels.Test.Dashboard
{
    public class DashboardPageDetailViewModelTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IDashboardUseCase>(() => DashboardUseCaseBuilder.Instance().Build());

            var viewModel = new DashboardPageDetailViewModel(useCase, navigation);

            viewModel.DateChangedCommand.Should().NotBeNull();
            viewModel.ViewAllTasksCommand.Should().NotBeNull();
            viewModel.FloatActionCommand.Should().NotBeNull();
            viewModel.SelectedCategoryToShowDetailsCommand.Should().NotBeNull();
        }

        [Fact]
        public void Validade_OnNavigatedFrom()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IDashboardUseCase>(() => DashboardUseCaseBuilder.Instance().Build());

            var viewModel = new DashboardPageDetailViewModel(useCase, navigation);

            Action action = () => viewModel.OnNavigatedFrom(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_FloatAction()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IDashboardUseCase>(() => DashboardUseCaseBuilder.Instance().Build());

            var viewModel = new DashboardPageDetailViewModel(useCase, navigation);

            Action action = () => viewModel.FloatActionCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public async Task Validade_Initialize()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IDashboardUseCase>(() => DashboardUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new DashboardPageDetailViewModel(useCase, navigation);

            Func<Task> action = async () => await viewModel.InitializeAsync(null);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public void Validade_Command_ViewAllTasks()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IDashboardUseCase>(() => DashboardUseCaseBuilder.Instance().Build());

            var viewModel = new DashboardPageDetailViewModel(useCase, navigation)
            {
                Model = new Timerom.App.Model.DashboardDateModel
                {
                    Date = DateTime.Now,
                    Dashboard = RequestDashboardModel.Instance().Build()
                }
            };

            Action action = () => viewModel.ViewAllTasksCommand.Execute(null);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_SelectedCategoryToShowDetails()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IDashboardUseCase>(() => DashboardUseCaseBuilder.Instance().Build());

            var viewModel = new DashboardPageDetailViewModel(useCase, navigation)
            {
                Model = new Timerom.App.Model.DashboardDateModel
                {
                    Date = DateTime.Now,
                    Dashboard = RequestDashboardModel.Instance().Build()
                }
            };

            Action action = () => viewModel.SelectedCategoryToShowDetailsCommand.Execute(viewModel.Model.Dashboard.Tasks.First());

            action.Should().NotThrow();
        }

        [Fact]
        public void Validate_OnNavigatedTo()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IDashboardUseCase>(() => DashboardUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new DashboardPageDetailViewModel(useCase, navigation)
            {
                Model = new Timerom.App.Model.DashboardDateModel
                {
                    Date = DateTime.Now,
                    Dashboard = RequestDashboardModel.Instance().Build()
                }
            };

            var parameters = INavigationParametersBuilder.Instance()
                .Parameter("Refresh", 1)
                .Build();

            Action action = () => viewModel.OnNavigatedTo(parameters);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validade_Command_DateChanged()
        {
            var navigation = new Lazy<INavigationService>(() => INavigationServiceBuilder.Instance().Build());
            var useCase = new Lazy<IDashboardUseCase>(() => DashboardUseCaseBuilder.Instance().Execute().Build());

            var viewModel = new DashboardPageDetailViewModel(useCase, navigation)
            {
                Model = new Timerom.App.Model.DashboardDateModel
                {
                    Date = DateTime.Now,
                    Dashboard = RequestDashboardModel.Instance().Build()
                }
            };

            Action action = () => viewModel.DateChangedCommand.Execute(DateTime.UtcNow);

            action.Should().NotThrow();
        }
    }
}
