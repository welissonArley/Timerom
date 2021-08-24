using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Timerom.App.ViewModels.AboutThisProject
{
    public class IlustrationsInformationsViewModel : ViewModelBase
    {
        public ICommand LinkCommand { get; private set; }

        public IlustrationsInformationsViewModel()
        {
            LinkCommand = new Command((url) =>
            {
                Launcher.OpenAsync(new Uri(url.ToString()));
            });
        }
    }
}
