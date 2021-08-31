using System;
using System.Collections.ObjectModel;

namespace Timerom.App.Model
{
    public class TasksDetailsModel
    {
        public DateTime Date { get; set; }
        public ObservableCollection<TaskModel> Tasks { get; set; }
    }
}
