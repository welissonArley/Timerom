using System.Collections.ObjectModel;

namespace Timerom.App.Model
{
    public class ActivityAnalyticStatisticsModel
    {
        public TaskAnalyticModel Productive { get; set; }
        public TaskAnalyticModel Neutral { get; set; }
        public TaskAnalyticModel Unproductive { get; set; }
        public ObservableCollection<ActivitiesAnalyticModel> Activities { get; set; }
    }
}
