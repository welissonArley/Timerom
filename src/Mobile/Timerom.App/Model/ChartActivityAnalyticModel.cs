using System;
using Timerom.App.ValueObjects.Enuns;

namespace Timerom.App.Model
{
    public class ChartActivityAnalyticModel
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public CategoryType Type { get; set; }
    }
}
