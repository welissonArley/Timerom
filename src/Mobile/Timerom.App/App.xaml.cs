using Timerom.App.Views.Views.Home;
using Xamarin.Forms;

namespace Timerom.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetAppTheme();

            MainPage = new HomePage();
        }

        private void SetAppTheme()
        {
            Current.UserAppTheme = Current.RequestedTheme == OSAppTheme.Unspecified ? OSAppTheme.Light : Current.RequestedTheme;
        }
    }
}
