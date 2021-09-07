using System;
using System.Threading.Tasks;

namespace Timerom.App.UseCase.UserTask.Interfaces
{
    public interface ILastTaskTimeForTodayUseCase
    {
        Task<DateTime> Execute();
    }
}
