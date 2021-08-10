using Foundation;
using Timerom.App.iOS.AppVersion;
using Timerom.App.Services.AppVersion;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersioniOS))]
namespace Timerom.App.iOS.AppVersion
{
    public class AppVersioniOS : IAppVersion
    {
        public string GetVersionNumber()
        {
            var info = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"];

            return $"{info.Description}.0";
        }
    }
}