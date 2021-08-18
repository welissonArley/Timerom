using Prism;
using Prism.Ioc;
using Timerom.App.iOS.AppVersion;
using Timerom.App.Services.AppVersion;

namespace Timerom.App.iOS.Settings
{
    public class iOSPlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped<IAppVersion, AppVersioniOS>();
        }
    }
}