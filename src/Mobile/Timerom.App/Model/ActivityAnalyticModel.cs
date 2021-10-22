namespace Timerom.App.Model
{
    public class ActivityAnalyticModel
    {
        public TaskAnalyticModel Total { get; set; }
        public TaskAnalyticModel Productive { get; set; }
        public TaskAnalyticModel Neutral { get; set; }
        public TaskAnalyticModel Unproductive { get; set; }
    }
}
