using System;
using Timerom.App.Services.XamarinEssentials.Interface;

namespace Timerom.App.Services.XamarinEssentials
{
    public class Preferences : IPreferences
    {
        public bool ContainsKey(string key)
        {
            return Xamarin.Essentials.Preferences.ContainsKey(key);
        }

        public bool Get(string key, bool defaultValue)
        {
            return Xamarin.Essentials.Preferences.Get(key, defaultValue);
        }

        public string Get(string key, string defaultValue)
        {
            return Xamarin.Essentials.Preferences.Get(key, defaultValue);
        }

        public DateTime Get(string key, DateTime defaultValue)
        {
            return Xamarin.Essentials.Preferences.Get(key, defaultValue);
        }

        public long Get(string key, long defaultValue)
        {
            return Xamarin.Essentials.Preferences.Get(key, defaultValue);
        }

        public void Remove(string key)
        {
            Xamarin.Essentials.Preferences.Remove(key);
        }

        public void Set(string key, string value)
        {
            Xamarin.Essentials.Preferences.Set(key, value);
        }

        public void Set(string key, bool value)
        {
            Xamarin.Essentials.Preferences.Set(key, value);
        }

        public void Set(string key, DateTime value)
        {
            Xamarin.Essentials.Preferences.Set(key, value);
        }

        public void Set(string key, long value)
        {
            Xamarin.Essentials.Preferences.Set(key, value);
        }
    }
}
