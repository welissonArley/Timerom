using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Dto;
using Timerom.App.Views.Views.Reports.ParetoPrinciple;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Reports.ParetoPrinciple
{
    public class ParetoPrincipleResultViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IGetParetoPrincipleResultUseCase> useCase;
        private IGetParetoPrincipleResultUseCase _useCase => useCase.Value;

        private ParetoPrincipleFilter _filter;

        public string Period { get; set; }
        public ParetoPrincipleModel Model { get; set; }

        public IAsyncCommand<long> ItemSelectedCommand { get; private set; }

        public ParetoPrincipleResultViewModel(Lazy<INavigationService> navigationService,
            Lazy<IGetParetoPrincipleResultUseCase> useCase) : base(navigationService)
        {
            this.useCase = useCase;

            ItemSelectedCommand = new AsyncCommand<long>(ItemSelectedCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private async Task ItemSelectedCommandExecuted(long categoryId)
        {
            var navParameters = new NavigationParameters
            {
                { "Period", $"{_filter.StartsAt.ToShortDateString()} - {_filter.EndsAt.ToShortDateString()}" },
                { "Filter", new ParetoPrincipleFilter { StartsAt = _filter.StartsAt, EndsAt = _filter.EndsAt, CategoryId = categoryId } }
            };

            await _navigationService.NavigateAsync(nameof(ParetoPrincipleResultPage), navParameters);
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            Period = parameters.GetValue<string>("Period");
            _filter = parameters.GetValue<ParetoPrincipleFilter>("Filter");

            Model = await _useCase.Execute(_filter);

            RaisePropertyChanged("Model");
            RaisePropertyChanged("Period");
        }
    }
}
