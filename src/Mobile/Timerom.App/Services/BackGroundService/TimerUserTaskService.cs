using Matcha.BackgroundService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Timerom.App.Services.BackGroundService
{
    public class TimerUserTaskService : IPeriodicTask
    {
        private static bool IsRun { get; set; }
        private static Model.Category SubcategoryModel { get; set; }

        private static DateTime StartsAt { get; set; }
        private static int Time { get; set; }
        private static IList<ICommand> _enrolled;

        public TimeSpan Interval { get; set; }

        public TimerUserTaskService(int seconds, Model.Category subcategory)
        {
            IsRun = true;
            Interval = TimeSpan.FromSeconds(seconds);
            StartsAt = DateTime.Now;
            _enrolled = new List<ICommand>();
            SubcategoryModel = subcategory;
        }

        public static void Subscribe(ICommand command)
        {
            if (!_enrolled.Any())
                Time = (int)(DateTime.Now - StartsAt).TotalSeconds;

            _enrolled.Add(command);
        }

        public async Task<bool> StartJob()
        {
            Time++;

            foreach(var command in _enrolled)
                command.Execute(Time);

            return true;
        }

        public static bool IsRunning()
        {
            return IsRun;
        }
        public static void StopJob()
        {
            IsRun = false;
            _enrolled.Clear();
        }
        public static Model.Category Subcategory()
        {
            return SubcategoryModel;
        }
        public static DateTime TimerStartsAt()
        {
            return StartsAt;
        }
    }
}
