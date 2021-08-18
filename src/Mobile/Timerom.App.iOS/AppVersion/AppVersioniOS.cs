using Foundation;
using Timerom.App.Services.AppVersion;

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