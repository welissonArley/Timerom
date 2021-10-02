using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;

namespace Timerom.App.ViewModels.Reports.ActivityAnalytic
{
    public class ActivityAnalyticBarSeletedViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IActivityAnalyticDetailsUseCase> useCase;
        private IActivityAnalyticDetailsUseCase _useCase => useCase.Value;

        public string Description { get; private set; }

        public ActivityAnalyticStatisticsModel AnalyticModel { get; private set; }

        public ActivityAnalyticBarSeletedViewModel(Lazy<INavigationService> navigationService,
            Lazy<IActivityAnalyticDetailsUseCase> useCase) : base(navigationService)
        {
            this.useCase = useCase;
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var date = parameters.GetValue<DateTime>("Date");

            Description = string.Format(ResourceText.TITLE_YOUR_ARE_SEEING_WHICH_CATEGORIES_MAKE_UP_THE_RESULT, date.ToString("D"));

            AnalyticModel = await _useCase.Execute(date);

            RaisePropertyChanged("Description");
            RaisePropertyChanged("AnalyticModel");
        }
    }
}
