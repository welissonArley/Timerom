using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;   
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Tasks
{
    public class SelectCategoryForTaskViewModel : ViewModelBase, IInitializeAsync
    {
        #region UseCase
        private readonly Lazy<IGetAllCategoriesUseCase> useCase;
        private IGetAllCategoriesUseCase _useCase => useCase.Value;
        #endregion

        #region Commands
        public IAsyncCommand<string> SearchTextChangedCommand { get; private set; }
        public IAsyncCommand<Model.Category> ItemSelectedCommand { get; private set; }
        #endregion

        #region Models
        private ObservableCollection<Model.Category> _productiveCategories { get; set; }
        private ObservableCollection<Model.Category> _neutralCategories { get; set; }
        private ObservableCollection<Model.Category> _unproductiveCategories { get; set; }
        public ObservableCollection<Model.Category> ProductiveCategories { get; private set; }
        public ObservableCollection<Model.Category> NeutralCategories { get; private set; }
        public ObservableCollection<Model.Category> UnproductiveCategories { get; private set; }

        private OnSelectCategoryOptions _option { get; set; }
        #endregion

        public SelectCategoryForTaskViewModel(Lazy<IGetAllCategoriesUseCase> useCase, Lazy<INavigationService> navigationService) : base(navigationService)
        {
            this.useCase = useCase;

            SearchTextChangedCommand = new AsyncCommand<string>(OnSearchTextChanged);
            ItemSelectedCommand = new AsyncCommand<Model.Category>(ItemSelectedCommandExecuted, allowsMultipleExecutions: false);
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

        private async Task ItemSelectedCommandExecuted(Model.Category category)
        {
            var navParameters = new NavigationParameters
            {
                { "Category", category },
                { "Option", _option }
            };

            await _navigationService.NavigateAsync(nameof(SelectSubCategoryForTaskPage), navParameters);
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            try
            {
                _option = parameters.GetValue<OnSelectCategoryOptions>("Option");

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
