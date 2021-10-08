using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Views.Reports.ParetoPrinciple
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhatIsParetoPrinciplePage : ContentPage
    {
        private const string PARETO_PRINCIPLE = "https://www.investopedia.com/terms/1/80-20-rule.asp";

        public WhatIsParetoPrinciplePage()
        {
            InitializeComponent();

            var height = Application.Current.MainPage.Width - 40;

            Illustration.HeightRequest = height;
            Illustration.WidthRequest = height * 0.5582;

            LabelSource.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new AsyncCommand(LinkCommandExecuted, allowsMultipleExecutions: false)
            });
        }

        private async Task LinkCommandExecuted()
        {
            await Launcher.OpenAsync(new Uri(PARETO_PRINCIPLE));
        }
    }
}