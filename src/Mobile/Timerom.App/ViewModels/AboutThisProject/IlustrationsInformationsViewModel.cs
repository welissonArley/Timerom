using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

namespace Timerom.App.ViewModels.AboutThisProject
{
    public class IlustrationsInformationsViewModel : BindableBase
    {
        public IAsyncCommand<string> LinkCommand { get; private set; }

        public IlustrationsInformationsViewModel()
        {
            LinkCommand = new AsyncCommand<string>(LinkCommandExecuted, allowsMultipleExecutions: false);
        }

        private async Task LinkCommandExecuted(string url)
        {
            await Launcher.OpenAsync(new Uri(url));
        }
    }
}
