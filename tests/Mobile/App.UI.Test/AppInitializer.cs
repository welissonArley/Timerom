using Xamarin.UITest;

namespace App.UI.Test
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
                return ConfigureApp.Android.EnableLocalScreenshots().InstalledApp("com.id1tech.timerom.app").StartApp();

            return ConfigureApp.iOS.StartApp();
        }
    }
}