using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class SelectCategoryToUpdateViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<IGetAllCategoriesUseCase> useCase;
        private IGetAllCategoriesUseCase _useCase => useCase.Value;

        public IAsyncCommand<string> SearchTextChangedCommand { get; private set; }

        private ObservableCollection<Model.Category> _productiveCategories { get; set; }
        private ObservableCollection<Model.Category> _neutralCategories { get; set; }
        private ObservableCollection<Model.Category> _unproductiveCategories { get; set; }
        public ObservableCollection<Model.Category> ProductiveCategories { get; private set; }
        public ObservableCollection<Model.Category> NeutralCategories { get; private set; }
        public ObservableCollection<Model.Category> UnproductiveCategories { get; private set; }

        public SelectCategoryToUpdateViewModel(Lazy<IGetAllCategoriesUseCase> useCase, Lazy<INavigationService> navigationService) : base(navigationService)
        {
            this.useCase = useCase;

            SearchTextChangedCommand = new AsyncCommand<string>(OnSearchTextChanged);
        }

        private Task OnSearchTextChanged(string value)
        {
            ProductiveCategories = new ObservableCollection<Model.Category>(_productiveCategories.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());
            NeutralCategories = new ObservableCollection<Model.Category>(_neutralCategories.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());
            UnproductiveCategories = new ObservableCollection<Model.Category>(_unproductiveCategories.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());

            RaisePropertyChanged("ProductiveCategories");
            RaisePropertyChanged("NeutralCategories");
            RaisePropertyChanged("UnproductiveCategories");

            return Task.CompletedTask;
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            try
            {
                var response = await _useCase.Execute();

                _productiveCategories = new ObservableCollection<Model.Category>(response.Productive);
                _neutralCategories = new ObservableCollection<Model.Category>(response.Neutral);
                _unproductiveCategories = new ObservableCollection<Model.Category>(response.Unproductive);

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
