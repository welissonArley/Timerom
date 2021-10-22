using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmActionModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ConfirmActionModal()
        {
            InitializeComponent();

            CloseWhenBackgroundIsClicked = false;
        }
    }
}