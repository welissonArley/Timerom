using System;

namespace Timerom.App.Model
{
    public class RankingParetoPrincipleModel
    {
        public int Index { get; set; }
        public Category Category { get; set; }
        public int AmountOfTasks { get; set; }
        public double Percentage { get; set; }
        public TimeSpan Time { get; set; }
        public double AccumulatedPercentage { get; set; }
        public bool IsPartOf80Percent { get; set; }
    }
}
