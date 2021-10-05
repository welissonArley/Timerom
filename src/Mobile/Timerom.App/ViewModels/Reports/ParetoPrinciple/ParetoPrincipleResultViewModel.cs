using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Dto;

namespace Timerom.App.ViewModels.Reports.ParetoPrinciple
{
    public class ParetoPrincipleResultViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IGetParetoPrincipleResultUseCase> useCase;
        private IGetParetoPrincipleResultUseCase _useCase => useCase.Value;

        private ParetoPrincipleFilter _filter;

        public string Period { get; set; }
        public ParetoPrincipleModel Model { get; set; }

        public ParetoPrincipleResultViewModel(Lazy<INavigationService> navigationService,
            Lazy<IGetParetoPrincipleResultUseCase> useCase) : base(navigationService)
        {
            this.useCase = useCase;
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
