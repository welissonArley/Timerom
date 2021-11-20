using System;
using Timerom.App.Services.XamarinEssentials.Interface;

namespace Timerom.App.Services.BackGroundService
{
    public class UserTaskTimer : ITimerUserTask
    {
        private const string IsRunKey = "IsRun";

        private readonly Lazy<IPreferences> preferences;
        private IPreferences _preferences => preferences.Value;

        public UserTaskTimer(Lazy<IPreferences> preferences)
        {
            this.preferences = preferences;
        }

        public bool IsRunning()
        {
            return _preferences.Get(IsRunKey, false);
        }
    }
}
