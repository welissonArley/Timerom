using System;

namespace Timerom.App.Model
{
    public class TaskModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public double Percentage { get; set; }
        public Category Category { get; set; }
    }
}
