using System;
using System.Threading.Tasks;
using Timerom.App.Repository;
using Timerom.App.UseCase.UserTask.Interfaces;

namespace Timerom.App.UseCase.UserTask.Local.Insert
{
    public class LastTaskTimeForTodayUseCase : ILastTaskTimeForTodayUseCase
    {
        public async Task<DateTime> Execute()
        {
            UserTaskDatabase database = await UserTaskDatabase.Instance();

            var model = await database.GetLast(DateTime.Now);

            return model == null ? DateTime.Now : model.EndsAt;
        }
    }
}
