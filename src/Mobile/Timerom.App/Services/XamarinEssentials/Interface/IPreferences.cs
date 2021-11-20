namespace Timerom.App.Services.XamarinEssentials.Interface
{
    public interface IPreferences
    {
        bool Get(string key, bool defaultValue);
    }
}
