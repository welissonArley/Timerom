using System;
using Timerom.App.ValueObjects.Enuns;

namespace Timerom.App.Model
{
    public class ActivitiesAnalyticModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public CategoryType Type { get; set; }
        public double Percentage { get; set; }
    }
}
