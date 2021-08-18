using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : FlyoutPage, IFlyoutPageOptions
    {
		public bool IsPresentedAfterNavigation => false;
		
        public HomePage()
        {
            InitializeComponent();
        }
    }
}