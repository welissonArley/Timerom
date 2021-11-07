using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.ValueObjects.Dto;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Views.Reports.ParetoPrinciple;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Reports.ParetoPrinciple
{
    public class ChooseDatesParetoPrincipleViewModel : ViewModelBase
    {
        public bool CustomDateIsExpanded { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }

        public SelectDateParetoPrincipleOptions Option { get; set; }

        public IAsyncCommand<SelectDateParetoPrincipleOptions> OptionCommand { get; private set; }
        public IAsyncCommand ExecuteCommand { get; private set; }
        public IAsyncCommand WhatIsParetoPrincipleCommand { get; private set; }

        public ChooseDatesParetoPrincipleViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            Option = SelectDateParetoPrincipleOptions.Last7Days;
            StartsAt = DateTime.Today.AddDays(-7);
            EndsAt = DateTime.Today;

            OptionCommand = new AsyncCommand<SelectDateParetoPrincipleOptions>(OptionCommandExecuted, allowsMultipleExecutions: false);
            ExecuteCommand = new AsyncCommand(ExecuteCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
            WhatIsParetoPrincipleCommand = new AsyncCommand(WhatIsParetoPrincipleCommandExecuted, onException: HandleException, allowsMultipleExecutions: false);
        }

        private Task OptionCommandExecuted(SelectDateParetoPrincipleOptions option)
        {
            if (option != SelectDateParetoPrincipleOptions.CustomDates)
            {
                CustomDateIsExpanded = false;
                RaisePropertyChanged("CustomDateIsExpanded");
            }

            Option = option;
            UpdateDates();

            RaisePropertyChanged("Option");
            
            return Task.CompletedTask;
        }

        private async Task WhatIsParetoPrincipleCommandExecuted()
        {
            TrackEvent("ChooseDatesParetoPrinciplePage", "WhatIsParetoPrinciplePage", EventFlag.Navigation);
            await _navigationService.NavigateAsync(nameof(WhatIsParetoPrinciplePage));
        }
        private async Task ExecuteCommandExecuted()
        {
            TrackEvent("ChooseDatesParetoPrinciplePage", $"ParetoPrincipleResultPage - {Enum.GetName(typeof(SelectDateParetoPrincipleOptions), Option)}", EventFlag.Navigation);

            var navParameters = new NavigationParameters
            {
                { "Period", $"{StartsAt.ToShortDateString()} - {EndsAt.ToShortDateString()}" },
                { "Filter", new ParetoPrincipleFilter { StartsAt = StartsAt, EndsAt = EndsAt } }
            };

            await _navigationService.NavigateAsync(nameof(ParetoPrincipleResultPage), navParameters);
        }

        private void UpdateDates()
        {
            switch (Option)
            {
                case SelectDateParetoPrincipleOptions.Last15Days:
                    {
                        StartsAt = DateTime.Today.AddDays(-15);
                        EndsAt = DateTime.Today;
                    }break;
                case SelectDateParetoPrincipleOptions.Last7Days:
                    {
                        StartsAt = DateTime.Today.AddDays(-7);
                        EndsAt = DateTime.Today;
                    }
                    break;
                case SelectDateParetoPrincipleOptions.LastMonth:
                    {
                        var firstDayMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);

                        StartsAt = firstDayMonth;
                        EndsAt = firstDayMonth.AddMonths(1).AddDays(-1);
                    }
                    break;
                case SelectDateParetoPrincipleOptions.ThisMonth:
                    {
                        var firstDayMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                        StartsAt = firstDayMonth;
                        EndsAt = firstDayMonth.AddMonths(1).AddDays(-1);
                    }
                    break;
            }

            RaisePropertyChanged("StartsAt");
            RaisePropertyChanged("EndsAt");
        }
    }
}
