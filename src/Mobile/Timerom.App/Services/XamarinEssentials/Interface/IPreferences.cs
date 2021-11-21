using System;

namespace Timerom.App.Services.XamarinEssentials.Interface
{
    public interface IPreferences
    {
        bool Get(string key, bool defaultValue);
        string Get(string key, string defaultValue);
        DateTime Get(string key, DateTime defaultValue);
        long Get(string key, long defaultValue);
        void Set(string key, string value);
        void Set(string key, bool value);
        void Set(string key, DateTime value);
        void Set(string key, long value);
        void Remove(string key);
        bool ContainsKey(string key);
    }
}
