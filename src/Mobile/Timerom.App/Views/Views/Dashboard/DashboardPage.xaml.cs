using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Views.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : FlyoutPage, IFlyoutPageOptions
    {
		public bool IsPresentedAfterNavigation => false;
		
        public DashboardPage()
        {
            InitializeComponent();
        }
    }
}