using System;
using Timerom.App.ValueObjects.Enuns;

namespace Timerom.App.Model
{
    public class DashboardTaskModel
    {
        public string Title { get; set; }
        public TimeSpan Hours { get; set; }
        public double Percentage { get; set; }
        public CategoryType Category { get; set; }
    }
}
