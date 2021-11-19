using System;
using System.Threading.Tasks;
using Timerom.App.Services.XamarinEssentials.Interface;

namespace Timerom.App.Services.XamarinEssentials
{
    public class Launcher : ILauncher
    {
        public async Task OpenAsync(Uri uri)
        {
            await Xamarin.Essentials.Launcher.OpenAsync(uri);
        }
    }
}
