using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using Timerom.App.Services.XamarinEssentials.Interface;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.AboutThisProject
{
    public class IlustrationsInformationsViewModel : BindableBase
    {
        public IAsyncCommand<string> LinkCommand { get; private set; }

        private readonly Lazy<ILauncher> launcher;
        private ILauncher _launcher => launcher.Value;

        public IlustrationsInformationsViewModel(Lazy<ILauncher> launcher)
        {
            this.launcher = launcher;
            LinkCommand = new AsyncCommand<string>(LinkCommandExecuted, allowsMultipleExecutions: false);
        }

        private async Task LinkCommandExecuted(string url)
        {
            await _launcher.OpenAsync(new Uri(url));
        }
    }
}
