using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class CategoriesViewModel : BindableBase, IInitializeAsync
    {
        private readonly Lazy<IGetAllCategoriesUseCase> useCase;
        private IGetAllCategoriesUseCase _useCase => useCase.Value;

        private readonly Lazy<INavigationService> navigationService;
        private INavigationService _navigationService => navigationService.Value;

        public ObservableCollection<Model.Category> ProductiveCategories { get; private set; }
        public ObservableCollection<Model.Category> NeutralCategories { get; private set; }
        public ObservableCollection<Model.Category> UnproductiveCategories { get; private set; }

        public IAsyncCommand FloatActionCommand { get; set; }

        public CategoriesViewModel(Lazy<IGetAllCategoriesUseCase> useCase, Lazy<INavigationService> navigationService)
        {
            this.useCase = useCase;
            this.navigationService = navigationService;

            FloatActionCommand = new AsyncCommand(FloatActionCommandExecute, allowsMultipleExecutions: false);
        }

        private async Task FloatActionCommandExecute()
        {
            await _navigationService.NavigateAsync("FloatActionCategoriesModal", useModalNavigation: true);
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var response = await _useCase.Execute();
            ProductiveCategories = new ObservableCollection<Model.Category>(response.Productive);
            NeutralCategories = new ObservableCollection<Model.Category>(response.Neutral);
            UnproductiveCategories = new ObservableCollection<Model.Category>(response.Unproductive);

            RaisePropertyChanged("ProductiveCategories");
            RaisePropertyChanged("NeutralCategories");
            RaisePropertyChanged("UnproductiveCategories");
        }
    }
}
