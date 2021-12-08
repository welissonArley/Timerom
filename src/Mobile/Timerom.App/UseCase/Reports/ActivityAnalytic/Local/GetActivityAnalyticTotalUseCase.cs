using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Reports.ActivityAnalytic.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ValueObjects.Functions;

namespace Timerom.App.UseCase.Reports.ActivityAnalytic.Local
{
    public class GetActivityAnalyticTotalUseCase : IGetActivityAnalyticTotalUseCase
    {
        private readonly FuncCorrectDate _funcCorrectDate;

        private readonly Lazy<ICategoryReadOnlyRepository> repositoryReadOnly;
        private readonly Lazy<IUserTaskReadOnlyRepository> repositoryUserTask;

        public GetActivityAnalyticTotalUseCase(Lazy<ICategoryReadOnlyRepository> repositoryReadOnly,
            Lazy<IUserTaskReadOnlyRepository> repositoryUserTask)
        {
            this.repositoryReadOnly = repositoryReadOnly;
            this.repositoryUserTask = repositoryUserTask;

            _funcCorrectDate = new FuncCorrectDate();
        }

        public async Task<ActivityAnalyticModel> Execute()
        {
            var activityAnalyticBase = new GetActivityAnalyticBase(repositoryReadOnly, repositoryUserTask);

            var startsAt = DateTime.Now.Date.AddDays(-7);
            var endsAt = DateTime.Now.Date;

            var userTasks = await activityAnalyticBase.GetUserTasks(startsAt, endsAt);

            var response = new ActivityAnalyticModel
            {
                Total = CreateModel(),
                Neutral = CreateModel(),
                Productive = CreateModel(),
                Unproductive = CreateModel()
            };

            var totalTime = 0;
            var productiveTotalTime = 0;
            var neutralTotalTime = 0;
            var unproductiveTotalTime = 0;

            for (var date = startsAt; date <= endsAt; date = date.AddDays(1))
            {
                var tasksDay = userTasks.Where(c => c.StartsAt.Date == date.Date || c.EndsAt.Date == date.Date);

                if (tasksDay.Any())
                {
                    var productiveTasks = tasksDay.Where(c => c.Category.Type == CategoryType.Productive);
                    var neutralTasks = tasksDay.Where(c => c.Category.Type == CategoryType.Neutral);
                    var unproductiveTasks = tasksDay.Where(c => c.Category.Type == CategoryType.Unproductive);

                    response.Total.AmountOfTasks += tasksDay.Count();
                    response.Productive.AmountOfTasks += productiveTasks.Count();
                    response.Neutral.AmountOfTasks += neutralTasks.Count();
                    response.Unproductive.AmountOfTasks += unproductiveTasks.Count();

                    totalTime += TotalTime(tasksDay, date);
                    productiveTotalTime += TotalTime(productiveTasks, date);
                    neutralTotalTime += TotalTime(neutralTasks, date);
                    unproductiveTotalTime += TotalTime(unproductiveTasks, date);
                }
            }

            response.Total.Time = new TimeSpan(hours: 0, minutes: totalTime, seconds: 0);
            response.Productive.Time = new TimeSpan(hours: 0, minutes: productiveTotalTime, seconds: 0);
            response.Neutral.Time = new TimeSpan(hours: 0, minutes: neutralTotalTime, seconds: 0);
            response.Unproductive.Time = new TimeSpan(hours: 0, minutes: unproductiveTotalTime, seconds: 0);

            return response;
        }

        private TaskAnalyticModel CreateModel()
        {
            return new TaskAnalyticModel
            {
                AmountOfTasks = 0
            };
        }

        private int TotalTime(IEnumerable<TaskModel> userTasks, DateTime searchDate)
        {
            return (int)userTasks.Sum(c => (_funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Ends - _funcCorrectDate.CorrectDate(c.StartsAt, c.EndsAt, searchDate).Starts).TotalMinutes);
        }
    }
}
