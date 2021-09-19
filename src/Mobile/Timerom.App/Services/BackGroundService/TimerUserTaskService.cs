using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Timerom.App.Services.BackGroundService
{
    public class TimerUserTaskService
    {
        private const string IsRunKey = "IsRun";
        private const string TaskTitleKey = "TaskTitle";
        private const string StartsAtKey = "StartsAt";
        private const string SubcategoryIdKey = "SubcategoryId";

        private int Time { get; set; }
        private ICommand _callback;
        private bool _continue { get; set; }

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
            Preferences.Set(IsRunKey, true);

            if (!Preferences.ContainsKey(StartsAtKey))
                Preferences.Set(StartsAtKey, DateTime.Now);

            var startsAt = TimerStartsAt();

            Preferences.Set(SubcategoryIdKey, subcategory.Id);

            Time = (int)(DateTime.Now - startsAt).TotalSeconds;

            _continue = true;

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                Time++;

                _callback?.Execute(Time);

                return _continue;
            });
        }
        public void StopJob()
        {
            _continue = false;

            Preferences.Remove(SubcategoryIdKey);
            Preferences.Remove(StartsAtKey);
            Preferences.Remove(TaskTitleKey);
            Preferences.Remove(IsRunKey);

            _callback = null;
        }

        public static bool IsRunning()
        {
            return Preferences.Get(IsRunKey, false);
        }
        public long SubcategoryId()
        {
            return Preferences.Get(SubcategoryIdKey, 0L);
        }
        public DateTime TimerStartsAt()
        {
            return Preferences.Get(StartsAtKey, DateTime.Now);
        }
        public string GetTitle()
        {
            return Preferences.Get(TaskTitleKey, "");
        }
        public void SetTitle(string title)
        {
            Preferences.Set(TaskTitleKey, title);
        }
        public int GetTime()
        {
            var startsAt = TimerStartsAt();

            return (int)(DateTime.Now - startsAt).TotalSeconds;
        }
    }
}
