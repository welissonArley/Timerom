using Prism;
using Prism.Ioc;
using Timerom.App.Droid.AppVersion;
using Timerom.App.Services.AppVersion;

namespace Timerom.App.Droid.Settings
{
    public class AndroidPlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped<IAppVersion, AppVersionAndroid>();
        }
    }
}