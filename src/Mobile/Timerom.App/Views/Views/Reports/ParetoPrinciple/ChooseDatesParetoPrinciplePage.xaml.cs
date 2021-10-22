using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Views.Reports.ParetoPrinciple
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseDatesParetoPrinciplePage : ContentPage
    {
        public ChooseDatesParetoPrinciplePage()
        {
            InitializeComponent();

            LabelLastMonth.Text = DateTime.Today.AddMonths(-1).ToString("MMMM");
        }
    }
}