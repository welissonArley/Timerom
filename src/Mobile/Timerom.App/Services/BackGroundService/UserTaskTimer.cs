using System;
using System.Windows.Input;
using Timerom.App.Services.XamarinEssentials.Interface;

namespace Timerom.App.Services.BackGroundService
{
    public class UserTaskTimer : ITimerUserTask
    {
        private const string IsRunKey = "IsRun";
        private const string TaskTitleKey = "TaskTitle";
        private const string StartsAtKey = "StartsAt";
        private const string SubcategoryIdKey = "SubcategoryId";

        private readonly Lazy<IPreferences> preferences;
        private IPreferences _preferences => preferences.Value;

        private int Time { get; set; }
        private ICommand _callback;
        private bool _continue { get; set; }

        public UserTaskTimer(Lazy<IPreferences> preferences)
        {
            this.preferences = preferences;
        }

        public bool IsRunning()
        {
            return _preferences.Get(IsRunKey, false);
        }

        public int GetTotalSeconds()
        {
            var startsAt = TimerStartsAt();

            return (int)(DateTime.Now - startsAt).TotalSeconds;
        }

        public string GetTitle()
        {
            return _preferences.Get(TaskTitleKey, "");
        }

        public void SetTitle(string title)
        {
            _preferences.Set(TaskTitleKey, title);
        }

        public DateTime TimerStartsAt()
        {
            return _preferences.Get(StartsAtKey, DateTime.Now);
        }

        public long SubcategoryId()
        {
            return _preferences.Get(SubcategoryIdKey, 0L);
        }

        public void Subscribe(ICommand command)
        {
            _callback = command;
        }
        public void Unsubscribe()
        {
            _continue = false;
            _callback = null;
        }
        public void StartJob(Model.Category subcategory)
        {
            _preferences.Set(IsRunKey, true);

            if (!_preferences.ContainsKey(StartsAtKey))
                _preferences.Set(StartsAtKey, DateTime.Now);

            var startsAt = TimerStartsAt();

            _preferences.Set(SubcategoryIdKey, subcategory.Id);

            Time = (int)(DateTime.Now - startsAt).TotalSeconds;

            _continue = true;

            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                Time++;

                _callback?.Execute(Time);

                return _continue;
            });
        }
        public void StopJob()
        {
            _continue = false;

            _preferences.Remove(SubcategoryIdKey);
            _preferences.Remove(StartsAtKey);
            _preferences.Remove(TaskTitleKey);
            _preferences.Remove(IsRunKey);

            _callback = null;
        }
    }
}
