﻿using System;
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

            var startsAt = DateTime.Now.Date.AddDays(-7);
            var endsAt = DateTime.Now.Date;

            var userTasks = await activityAnalyticBase.GetUserTasks(startsAt, endsAt);

            var response = new List<ChartActivityAnalyticModel>();

            for (var date = startsAt; date <= endsAt; date = date.AddDays(1))
            {
                var tasksDay = userTasks.Where(c => c.StartsAt.Date == date.Date || c.EndsAt.Date == date.Date);

                if(tasksDay.Any())
                    response.Add(CalculateModel(date, tasksDay));
            }

            return response.OrderBy(c => c.Date.Date).ToList();
        }

        private ChartActivityAnalyticModel CalculateModel(DateTime date, IEnumerable<TaskModel> userTasks)
        {
            var productiveTotalTime = TotalTime(userTasks.Where(c => c.Category.Type == CategoryType.Productive), date);
            var neutralTotalTime = TotalTime(userTasks.Where(c => c.Category.Type == CategoryType.Neutral), date);
            var unproductiveTotalTime = TotalTime(userTasks.Where(c => c.Category.Type == CategoryType.Unproductive), date);

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

        private double TotalTime(IEnumerable<TaskModel> userTasks, DateTime searchDate)
        {
            return userTasks.Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);
        }
    }
}
