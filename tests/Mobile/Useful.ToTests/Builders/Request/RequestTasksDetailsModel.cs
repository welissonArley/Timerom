using Bogus;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Timerom.App.Model;

namespace Useful.ToTests.Builders.Request
{
    public class RequestTasksDetailsModel
    {
        private static RequestTasksDetailsModel _instance;

        public static RequestTasksDetailsModel Instance()
        {
            _instance = new RequestTasksDetailsModel();
            return _instance;
        }

        public TasksDetailsModel Build()
        {
            return new Faker<TasksDetailsModel>()
                .RuleFor(u => u.Date, () => DateTime.Now)
                .RuleFor(u => u.Tasks, () => new ObservableCollection<TaskModel>
                {
                    RequestTask.Instance().Build()
                })
                .RuleFor(u => u.Filters, (f, u) => new ObservableCollection<FilterModel>(u.Tasks.Select(c => new FilterModel
                {
                    Name = c.Title
                })));
        }
    }
}
