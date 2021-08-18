using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Timerom.App.ViewModels.AboutThisProject
{
    public class IlustrationsInformationsViewModel : Prism.Mvvm.BindableBase
    {
        public ICommand LinkCommand { protected set; get; }

        public IlustrationsInformationsViewModel()
        {
            LinkCommand = new Command((url) =>
            {
                Launcher.OpenAsync(new Uri(url.ToString()));
            });
        }
    }
}
