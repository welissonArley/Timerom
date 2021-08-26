using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.Views.Views.Category;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class SelectCategoryToUpdateViewModel : ViewModelBase, IInitializeAsync, INavigationAware
    {
        private readonly Lazy<IGetAllCategoriesUseCase> useCase;
        private IGetAllCategoriesUseCase _useCase => useCase.Value;

        public IAsyncCommand<string> SearchTextChangedCommand { get; private set; }
        public IAsyncCommand<Model.Category> ItemSelectedCommand { get; private set; }

        private IList<Model.Category> _categoriesUpdated { get; set; }

        private ObservableCollection<Model.Category> _productiveCategories { get; set; }
        private ObservableCollection<Model.Category> _neutralCategories { get; set; }
        private ObservableCollection<Model.Category> _unproductiveCategories { get; set; }
        public ObservableCollection<Model.Category> ProductiveCategories { get; private set; }
        public ObservableCollection<Model.Category> NeutralCategories { get; private set; }
        public ObservableCollection<Model.Category> UnproductiveCategories { get; private set; }

        public SelectCategoryToUpdateViewModel(Lazy<IGetAllCategoriesUseCase> useCase, Lazy<INavigationService> navigationService) : base(navigationService)
        {
            this.useCase = useCase;

            _categoriesUpdated = new List<Model.Category>();

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
                { "Category", category }
            };

            await _navigationService.NavigateAsync(nameof(AddUpdateCategoryPage), navParameters);
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

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            if (_categoriesUpdated.Any())
                parameters.Add("Updated", _categoriesUpdated);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Updated"))
            {
                Model.Category categoryUpdated = parameters.GetValue<Model.Category>("Updated");
                UpdateCategories(categoryUpdated);
                _categoriesUpdated.Add(categoryUpdated);
            }
        }
        private void UpdateCategories(Model.Category categoryUpdated)
        {
            switch (categoryUpdated.Type)
            {
                case ValueObjects.Enuns.CategoryType.Productive:
                    {
                        var newList = NewListToUpdate(ProductiveCategories, categoryUpdated);

                        ProductiveCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        RaisePropertyChanged("ProductiveCategories");
                    }
                    break;
                case ValueObjects.Enuns.CategoryType.Neutral:
                    {
                        var newList = NewListToUpdate(NeutralCategories, categoryUpdated);

                        NeutralCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        RaisePropertyChanged("NeutralCategories");
                    }
                    break;
                case ValueObjects.Enuns.CategoryType.Unproductive:
                    {
                        var newList = NewListToUpdate(UnproductiveCategories, categoryUpdated);

                        UnproductiveCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        RaisePropertyChanged("UnproductiveCategories");
                    }
                    break;
            }
        }
        private List<Model.Category> NewListToUpdate(ObservableCollection<Model.Category> categories, Model.Category categoryUpdated)
        {
            List<Model.Category> newList = categories.Where(c => c.Id != categoryUpdated.Id).ToList();
            newList.Add(categoryUpdated);

            return newList;
        }
    }
}
