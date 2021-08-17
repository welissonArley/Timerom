using Timerom.App.ViewModels.AboutThisProject;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Views.AboutThisProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IlustrationsInformationsPage : ContentPage
    {
        public IlustrationsInformationsPage()
        {
            InitializeComponent();

            BindingContext = new IlustrationsInformationsViewModel();
        }
    }
}