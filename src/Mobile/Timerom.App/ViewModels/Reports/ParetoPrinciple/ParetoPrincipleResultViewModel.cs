using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.ViewModels.Reports.ParetoPrinciple
{
    public class ParetoPrincipleResultViewModel : ViewModelBase, IInitializeAsync
    {
        public ParetoPrincipleModel Model { get; set; }

        public ParetoPrincipleResultViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            
        }
    }
}
