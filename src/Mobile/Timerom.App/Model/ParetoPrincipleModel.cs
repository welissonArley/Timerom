using System.Collections.ObjectModel;

namespace Timerom.App.Model
{
    public class ParetoPrincipleModel
    {
        public double PercentageResult { get; set; }
        public double PercentageCause { get; set; }
        public ObservableCollection<RankingParetoPrincipleModel> Ranking { get; set; }
    }
}
