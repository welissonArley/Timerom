using Timerom.App.Droid.AppVersion;
using Timerom.App.Services.AppVersion;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersionAndroid))]
namespace Timerom.App.Droid.AppVersion
{
    public class AppVersionAndroid : IAppVersion
    {
        public string GetVersionNumber()
        {
            Android.Content.Context context = Android.App.Application.Context;
            Android.Content.PM.PackageManager manager = context.PackageManager;
            Android.Content.PM.PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return $"{info.VersionName}.0";
        }
    }
}