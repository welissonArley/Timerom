using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.Views.Views.Reports.ActivityAnalytic;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Reports.ActivityAnalytic
{
    public class ActivityAnalyticBarSeletedViewModel : ViewModelBase, IInitializeAsync
    {
        private DateTime _date { get; set; }

        private readonly Lazy<IActivityAnalyticDetailsUseCase> useCase;
        private IActivityAnalyticDetailsUseCase _useCase => useCase.Value;

        public string Description { get; private set; }

        public ActivityAnalyticStatisticsModel AnalyticModel { get; private set; }

        public IAsyncCommand<long> ActivitySelectedCommand { get; private set; }

        public ActivityAnalyticBarSeletedViewModel(Lazy<INavigationService> navigationService,
            Lazy<IActivityAnalyticDetailsUseCase> useCase) : base(navigationService)
        {
            this.useCase = useCase;

            ActivitySelectedCommand = new AsyncCommand<long>(ActivitySelectedCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private async Task ActivitySelectedCommandExecuted(long categoryId)
        {
            var navParameters = new NavigationParameters
            {
                { "Date", _date },
                { "ActivitiesAnalytic", AnalyticModel.Activities.First(c => c.Id == categoryId) }
            };

            await _navigationService.NavigateAsync(nameof(ActivityAnalyticBarSeletedCategoryPage), navParameters);
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            _date = parameters.GetValue<DateTime>("Date");

            Description = string.Format(ResourceText.TITLE_YOUR_ARE_SEEING_WHICH_CATEGORIES_MAKE_UP_THE_RESULT, _date.ToString("D"));

            AnalyticModel = await _useCase.Execute(_date);

            RaisePropertyChanged("Description");
            RaisePropertyChanged("AnalyticModel");
        }
    }
}
