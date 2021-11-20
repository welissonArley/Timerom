using Timerom.App.Services.XamarinEssentials.Interface;

namespace Timerom.App.Services.XamarinEssentials
{
    public class Preferences : IPreferences
    {
        public bool Get(string key, bool defaultValue)
        {
            return Xamarin.Essentials.Preferences.Get(key, false);
        }
    }
}
