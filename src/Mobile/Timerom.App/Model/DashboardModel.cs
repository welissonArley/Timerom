using System.Collections.ObjectModel;

namespace Timerom.App.Model
{
    public class DashboardModel
    {
        public DashboardModel()
        {
            Tasks = new ObservableCollection<DashboardTaskModel>();
        }

        public int ProductivePercentage { get; set; }
        public int NeutralPercentage { get; set; }
        public int UnproductivePercentage { get; set; }
        public int TotalTasks { get; set; }
        public ObservableCollection<DashboardTaskModel> Tasks { get; set; }
    }
}
