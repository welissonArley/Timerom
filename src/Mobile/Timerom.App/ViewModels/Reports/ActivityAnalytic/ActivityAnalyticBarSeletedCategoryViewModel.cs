using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace Timerom.App.ViewModels.Reports.ActivityAnalytic
{
    public class ActivityAnalyticBarSeletedCategoryViewModel : ViewModelBase, IInitializeAsync
    {
        public ActivityAnalyticBarSeletedCategoryViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            
        }
    }
}
