using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Functions;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Local
{
    public class GetActivityAnalyticTotalUseCase : IGetActivityAnalyticTotalUseCase
    {
        private readonly FuncCorrectDate _funcCorrectDate;

        public GetActivityAnalyticTotalUseCase()
        {
            _funcCorrectDate = new FuncCorrectDate();
        }

        public async Task<ActivityAnalyticModel> Execute()
        {
            var activityAnalyticBase = new GetActivityAnalyticBase();

            var userTasks = await activityAnalyticBase.GetUserTasks(DateTime.Now.Date.AddDays(-7), DateTime.Now.Date);

            return new ActivityAnalyticModel
            {
                Total = CalculateModel(userTasks),
                Productive = CalculateModel(userTasks.Where(c => c.Category.Type == ValueObjects.Enuns.CategoryType.Productive)),
                Neutral = CalculateModel(userTasks.Where(c => c.Category.Type == ValueObjects.Enuns.CategoryType.Neutral)),
                Unproductive = CalculateModel(userTasks.Where(c => c.Category.Type == ValueObjects.Enuns.CategoryType.Unproductive))
            };
        }

        private TaskAnalyticModel CalculateModel(IEnumerable<TaskModel> userTasks)
        {
            var searchDate = DateTime.Today;

            var timeTotal = userTasks.Sum(c =>
                (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends
                - 
                _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts)
                .TotalMinutes);

            return new TaskAnalyticModel
            {
                AmountOfTasks = userTasks.Count(),
                Time = new TimeSpan(hours: 0, seconds: 0, minutes: (int)timeTotal)
            };
        }
    }
}
