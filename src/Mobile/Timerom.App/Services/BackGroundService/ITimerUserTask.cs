using System;
using System.Windows.Input;

namespace Timerom.App.Services.BackGroundService
{
    public interface ITimerUserTask
    {
        bool IsRunning();
        int GetTotalSeconds();
        string GetTitle();
        void SetTitle(string title);
        DateTime TimerStartsAt();
        long SubcategoryId();

        void Subscribe(ICommand command);
        void Unsubscribe();
        void StartJob(Model.Category subcategory);
        void StopJob();
    }
}
