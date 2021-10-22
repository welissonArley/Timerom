using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;

namespace Timerom.App.ViewModels.Reports.ActivityAnalytic
{
    public class ActivityAnalyticBarSeletedCategoryViewModel : ViewModelBase, IInitializeAsync
    {
        public string Description { get; private set; }
        public ObservableCollection<ActivitiesAnalyticModel> Activities { get; set; }

        private readonly Lazy<IActivityAnalyticDetailsSubCategoryUseCase> useCase;
        private IActivityAnalyticDetailsSubCategoryUseCase _useCase => useCase.Value;

        public ActivityAnalyticBarSeletedCategoryViewModel(Lazy<INavigationService> navigationService,
            Lazy<IActivityAnalyticDetailsSubCategoryUseCase> useCase) : base(navigationService)
        {
            this.useCase = useCase;
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var date = parameters.GetValue<DateTime>("Date");
            var activitiesAnalytic = parameters.GetValue<ActivitiesAnalyticModel>("ActivitiesAnalytic");

            Description = string.Format(ResourceText.TITLE_YOUR_ARE_SEEING_WHICH_SUBCATEGORIES_MAKE_UP_THE_RESULT, activitiesAnalytic.Name, date.ToString("D"));

            Activities = await _useCase.Execute(activitiesAnalytic.Id, date);

            RaisePropertyChanged("Description");
            RaisePropertyChanged("Activities");
        }
    }
}
