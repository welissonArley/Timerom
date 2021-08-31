using System;
using Timerom.App.ValueObjects.Enuns;

namespace Timerom.App.Model
{
    public class TaskModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public double Percentage { get; set; }
        public CategoryType Type { get; set; }
    }
}
