using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OperationErrorModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public OperationErrorModal()
        {
            InitializeComponent();

            CloseWhenBackgroundIsClicked = false;
        }
    }
}