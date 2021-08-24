using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.Views.Modal.MenuOptions;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class CategoriesViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IGetAllCategoriesUseCase> useCase;
        private IGetAllCategoriesUseCase _useCase => useCase.Value;

        public ObservableCollection<Model.Category> ProductiveCategories { get; private set; }
        public ObservableCollection<Model.Category> NeutralCategories { get; private set; }
        public ObservableCollection<Model.Category> UnproductiveCategories { get; private set; }

        public IAsyncCommand FloatActionCommand { get; set; }

        public CategoriesViewModel(Lazy<IGetAllCategoriesUseCase> useCase, Lazy<INavigationService> navigationService) : base(navigationService)
        {
            this.useCase = useCase;

            FloatActionCommand = new AsyncCommand(FloatActionCommandExecute, allowsMultipleExecutions: false);
        }

        private async Task FloatActionCommandExecute()
        {
            await _navigationService.NavigateAsync(nameof(FloatActionCategoriesModal), useModalNavigation: true);
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            try
            {
                var response = await _useCase.Execute();
                ProductiveCategories = new ObservableCollection<Model.Category>(response.Productive);
                NeutralCategories = new ObservableCollection<Model.Category>(response.Neutral);
                UnproductiveCategories = new ObservableCollection<Model.Category>(response.Unproductive);

                RaisePropertyChanged("ProductiveCategories");
                RaisePropertyChanged("NeutralCategories");
                RaisePropertyChanged("UnproductiveCategories");
            }
            catch (System.Exception exception)
            {
                HandleException(exception);
            }
        }
    }
}
