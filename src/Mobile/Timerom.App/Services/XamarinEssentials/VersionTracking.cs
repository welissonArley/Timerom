using Timerom.App.Services.XamarinEssentials.Interface;

namespace Timerom.App.Services.XamarinEssentials
{
    public class VersionTracking : IVersionTracking
    {
        public string CurrentVersion()
        {
            return Xamarin.Essentials.VersionTracking.CurrentVersion;
        }
    }
}
