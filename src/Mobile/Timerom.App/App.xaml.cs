﻿using Prism.Ioc;
using Prism.Plugin.Popups;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ViewModels.AboutThisProject;
using Timerom.App.ViewModels.Category;
using Timerom.App.ViewModels.Dashboard;
using Timerom.App.ViewModels.Home;
using Timerom.App.ViewModels.Modal;
using Timerom.App.ViewModels.Modal.MenuOptions;
using Timerom.App.ViewModels.Tasks;
using Timerom.App.Views.Modal;
using Timerom.App.Views.Modal.MenuOptions;
using Timerom.App.Views.Views.AboutThisProject;
using Timerom.App.Views.Views.Category;
using Timerom.App.Views.Views.Dashboard;
using Timerom.App.Views.Views.Home;
using Timerom.App.Views.Views.Tasks;
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

            DashboardPageFlyoutViewModel.Initialize();
            //_ = await NavigationService.NavigateAsync("/DashboardPage/NavigationPage/DashboardPageDetail");
            _ = await NavigationService.NavigateAsync("/NavigationPage/HomePage");
        }

        private void SetAppTheme()
        {
            Current.UserAppTheme = Current.RequestedTheme == OSAppTheme.Unspecified ? OSAppTheme.Light : Current.RequestedTheme;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterPopupNavigationService();

            RegisterPages(containerRegistry);
            RegisterModals(containerRegistry);
            RegisterUseCases(containerRegistry);
        }

        private void RegisterPages(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<DashboardPage>();
            containerRegistry.RegisterForNavigation<DashboardPageDetail, DashboardPageDetailViewModel>();
            containerRegistry.RegisterForNavigation<DashboardPageFlyout, DashboardPageFlyoutViewModel>();
            containerRegistry.RegisterForNavigation<IlustrationsInformationsPage, IlustrationsInformationsViewModel>();
            containerRegistry.RegisterForNavigation<CategoriesPage, CategoriesViewModel>();
            containerRegistry.RegisterForNavigation<AddUpdateCategoryPage, AddUpdateCategoryViewModel>();
            containerRegistry.RegisterForNavigation<SelectCategoryToUpdatePage, SelectCategoryToUpdateViewModel>();
            containerRegistry.RegisterForNavigation<AddUpdateSubcategoryPage, AddUpdateSubcategoryViewModel>();
            containerRegistry.RegisterForNavigation<TaskDetailsPage, TaskDetailsViewModel>();
            containerRegistry.RegisterForNavigation<SelectCategoryForTaskPage, SelectCategoryForTaskViewModel>();
        }
        private void RegisterModals(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FloatActionCategoriesModal, FloatActionCategoriesViewModel>();
            containerRegistry.RegisterForNavigation<OperationErrorModal, OperationErrorModalViewModel>();
        }
        private void RegisterUseCases(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped<IGetAllCategoriesUseCase, UseCase.Categories.Local.GetAll.GetAllCategoriesUseCase>();
            containerRegistry.RegisterScoped<IInsertCategoryUseCase, UseCase.Categories.Local.Insert.InsertCategoryUseCase>();
            containerRegistry.RegisterScoped<IUpdateCategoryUseCase, UseCase.Categories.Local.Update.UpdateCategoryUseCase>();
            containerRegistry.RegisterScoped<IDeleteCategoryUseCase, UseCase.Categories.Local.Delete.DeleteCategoryUseCase>();
            containerRegistry.RegisterScoped<IInsertSubcategoryUseCase, UseCase.Categories.Local.Insert.InsertSubcategoryUseCase>();
            containerRegistry.RegisterScoped<IUpdateSubcategoryUseCase, UseCase.Categories.Local.Update.UpdateSubcategoryUseCase>();
            containerRegistry.RegisterScoped<IDeleteSubcategoryUseCase, UseCase.Categories.Local.Delete.DeleteSubcategoryUseCase>();
        }
    }
}
