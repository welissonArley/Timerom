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
    public class GetChartActivityAnalyticUseCase : IGetChartActivityAnalyticUseCase
    {
        private readonly FuncCorrectDate _funcCorrectDate;

        public GetChartActivityAnalyticUseCase()
        {
            _funcCorrectDate = new FuncCorrectDate();
        }

        public async Task<IList<ChartActivityAnalyticModel>> Execute()
        {
            var activityAnalyticBase = new GetActivityAnalyticBase();
            var userTasks = await activityAnalyticBase.GetUserTasks(DateTime.Now.Date.AddDays(-7), DateTime.Now.Date);

            var groupUserTasks = userTasks.GroupBy(c => c.StartsAt.Date);

            var response = new List<ChartActivityAnalyticModel>();

            foreach (var tasksDay in groupUserTasks)
                response.Add(CalculateModel(tasksDay.Key, tasksDay));

            return response.OrderBy(c => c.Date.Date).ToList();
        }

        private ChartActivityAnalyticModel CalculateModel(DateTime date, IEnumerable<TaskModel> userTasks)
        {
            var productiveTotalTime = TotalTime(userTasks.Where(c => c.Category.Type == ValueObjects.Enuns.CategoryType.Productive));
            var neutralTotalTime = TotalTime(userTasks.Where(c => c.Category.Type == ValueObjects.Enuns.CategoryType.Neutral));
            var unproductiveTotalTime = TotalTime(userTasks.Where(c => c.Category.Type == ValueObjects.Enuns.CategoryType.Unproductive));

            var maxValues = Max(productiveTotalTime, neutralTotalTime, unproductiveTotalTime);

            return new ChartActivityAnalyticModel
            {
                Date = date,
                Type = maxValues.Type,
                Time = new TimeSpan(hours: 0, seconds: 0, minutes: maxValues.TotalMinutes)
            };
        }

        private (CategoryType Type, int TotalMinutes) Max(int productiveTotalTime, int neutralTotalTime, int unproductiveTotalTime)
        {
            var value = Math.Max(Math.Max(productiveTotalTime, neutralTotalTime), unproductiveTotalTime);

            var category = value == productiveTotalTime ?
                CategoryType.Productive : value == neutralTotalTime ? CategoryType.Neutral : CategoryType.Unproductive;

            return (category, value);
        }

        private int TotalTime(IEnumerable<TaskModel> userTasks)
        {
            var searchDate = DateTime.Today;

            return (int)userTasks.Sum(c =>
                (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends
                -
                _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts)
                .TotalMinutes);
        }
    }
}
