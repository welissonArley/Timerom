using Prism.Ioc;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ViewModels.AboutThisProject;
using Timerom.App.ViewModels.Category;
using Timerom.App.ViewModels.Home;
using Timerom.App.Views.Views.AboutThisProject;
using Timerom.App.Views.Views.Category;
using Timerom.App.Views.Views.Home;
using Xamarin.Forms;

namespace Timerom.App
{
    public partial class App
    {
        public App(Prism.IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            SetAppTheme();

            _ = await NavigationService.NavigateAsync("/HomePage/NavigationPage/HomePageDetail");
        }

        private void SetAppTheme()
        {
            Current.UserAppTheme = Current.RequestedTheme == OSAppTheme.Unspecified ? OSAppTheme.Light : Current.RequestedTheme;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage>();
            containerRegistry.RegisterForNavigation<HomePageDetail>();
            containerRegistry.RegisterForNavigation<IlustrationsInformationsPage, IlustrationsInformationsViewModel>();
            containerRegistry.RegisterForNavigation<HomePageFlyout, HomePageFlyoutViewModel>();
            containerRegistry.RegisterForNavigation<CategoriesPage, CategoriesViewModel>();

            containerRegistry.RegisterScoped<IGetAllCategoriesUseCase, UseCase.Categories.Local.GetAll.GetAllCategoriesUseCase>();
        }
    }
}
