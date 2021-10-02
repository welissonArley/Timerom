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

            var dates = userTasks.Select(c => c.StartsAt.Date).Distinct();

            var response = new List<ChartActivityAnalyticModel>();

            foreach (var date in dates)
            {
                var tasksDay = userTasks.Where(c => c.StartsAt.Date == date.Date || c.EndsAt.Date == date.Date);

                response.Add(CalculateModel(date, tasksDay));
            }

            return response.OrderBy(c => c.Date.Date).ToList();
        }

        private ChartActivityAnalyticModel CalculateModel(DateTime date, IEnumerable<TaskModel> userTasks)
        {
            var productiveTotalTime = TotalTime(userTasks.Where(c => c.Category.Type == CategoryType.Productive));
            var neutralTotalTime = TotalTime(userTasks.Where(c => c.Category.Type == CategoryType.Neutral));
            var unproductiveTotalTime = TotalTime(userTasks.Where(c => c.Category.Type == CategoryType.Unproductive));

            var maxValues = Max(productiveTotalTime, neutralTotalTime, unproductiveTotalTime);

            return new ChartActivityAnalyticModel
            {
                Date = date,
                Type = maxValues.Type,
                Time = new TimeSpan(hours: 0, seconds: 0, minutes: maxValues.TotalMinutes)
            };
        }

        private (CategoryType Type, int TotalMinutes) Max(double productiveTotalTime, double neutralTotalTime, double unproductiveTotalTime)
        {
            var value = Math.Max(Math.Max(productiveTotalTime, neutralTotalTime), unproductiveTotalTime);

            var category = value == productiveTotalTime ?
                CategoryType.Productive : value == neutralTotalTime ? CategoryType.Neutral : CategoryType.Unproductive;

            return (category, (int)value);
        }

        private double TotalTime(IEnumerable<TaskModel> userTasks)
        {
            var searchDate = DateTime.Today;

            return userTasks.Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);
        }
    }
}
