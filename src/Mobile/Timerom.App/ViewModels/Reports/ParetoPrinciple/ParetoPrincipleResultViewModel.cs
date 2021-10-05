using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace Timerom.App.ViewModels.Reports.ParetoPrinciple
{
    public class ParetoPrincipleResultViewModel : ViewModelBase, IInitializeAsync
    {
        public ParetoPrincipleResultViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            
        }
    }
}
