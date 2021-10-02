using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ValueObjects.Functions;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Local
{
    public class ActivityAnalyticDetailsUseCase : IActivityAnalyticDetailsUseCase
    {
        private readonly FuncCorrectDate _funcCorrectDate;

        public ActivityAnalyticDetailsUseCase()
        {
            _funcCorrectDate = new FuncCorrectDate();
        }

        public async Task<ActivityAnalyticStatisticsModel> Execute(DateTime date)
        {
            var activityAnalyticBase = new GetActivityAnalyticBase();
            var userTasks = await activityAnalyticBase.GetUserTasks(date, date);

            var productiveTasks = userTasks.Where(c => c.Category.Type == CategoryType.Productive);
            var neutralTasks = userTasks.Where(c => c.Category.Type == CategoryType.Neutral);
            var unproductiveTasks = userTasks.Where(c => c.Category.Type == CategoryType.Unproductive);

            return new ActivityAnalyticStatisticsModel
            {
                Productive = new TaskAnalyticModel
                {
                    AmountOfTasks = productiveTasks.Count(),
                    Time = new TimeSpan(hours: 0, seconds: 0, minutes: TotalTime(productiveTasks, date))
                },
                Neutral = new TaskAnalyticModel
                {
                    AmountOfTasks = neutralTasks.Count(),
                    Time = new TimeSpan(hours: 0, seconds: 0, minutes: TotalTime(neutralTasks, date))
                },
                Unproductive = new TaskAnalyticModel
                {
                    AmountOfTasks = unproductiveTasks.Count(),
                    Time = new TimeSpan(hours: 0, seconds: 0, minutes: TotalTime(unproductiveTasks, date))
                }
            };
        }

        private int TotalTime(IEnumerable<TaskModel> userTasks, DateTime searchDate)
        {
            return (int)userTasks.Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);
        }
    }
}
