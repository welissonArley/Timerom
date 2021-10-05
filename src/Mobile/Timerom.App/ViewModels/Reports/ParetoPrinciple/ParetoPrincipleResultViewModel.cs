using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.ViewModels.Reports.ParetoPrinciple
{
    public class ParetoPrincipleResultViewModel : ViewModelBase, IInitializeAsync
    {
        public string Period { get; set; }
        public ParetoPrincipleModel Model { get; set; }

        public ParetoPrincipleResultViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            Period = parameters.GetValue<string>("Period");

            Model = new ParetoPrincipleModel
            {
                PercentageCause = 21.2,
                PercentageResult = 81.23,
                Ranking = new System.Collections.ObjectModel.ObservableCollection<RankingParetoPrincipleModel>
                {
                    new RankingParetoPrincipleModel
                    {
                        AccumulatedPercentage = 46,
                        IsPartOf80Percent = true,
                        AmountOfTasks = 27,
                        Index = 1,
                        Percentage = 46,
                        Time = new TimeSpan(hours: 0, minutes: 568, seconds: 0),
                        Category = new Model.Category
                        {
                            Name = "Study",
                            Type = ValueObjects.Enuns.CategoryType.Productive
                        }
                    },
                    new RankingParetoPrincipleModel
                    {
                        AccumulatedPercentage = 82,
                        IsPartOf80Percent = true,
                        AmountOfTasks = 27,
                        Index = 2,
                        Percentage = 36,
                        Time = new TimeSpan(hours: 0, minutes: 468, seconds: 0),
                        Category = new Model.Category
                        {
                            Name = "Social Media",
                            Type = ValueObjects.Enuns.CategoryType.Unproductive
                        }
                    },
                    new RankingParetoPrincipleModel
                    {
                        AccumulatedPercentage = 83,
                        IsPartOf80Percent = false,
                        AmountOfTasks = 27,
                        Index = 3,
                        Percentage = 1,
                        Time = new TimeSpan(hours: 0, minutes: 68, seconds: 0),
                        Category = new Model.Category
                        {
                            Name = "Cooking",
                            Type = ValueObjects.Enuns.CategoryType.Neutral
                        }
                    }
                }
            };

            RaisePropertyChanged("Model");
        }
    }
}
