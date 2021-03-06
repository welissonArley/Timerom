using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Modal.MenuOptions;
using Timerom.App.Views.Views.Category;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class CategoriesViewModel : ViewModelBase, IInitializeAsync, INavigationAware
    {
        #region UseCases
        private readonly Lazy<IGetAllCategoriesUseCase> useCase;
        private IGetAllCategoriesUseCase _useCase => useCase.Value;
        #endregion

        #region Models
        private List<Model.Category> _categories { get; set; }
        public ObservableCollection<Model.Category> ProductiveCategories { get; private set; }
        public ObservableCollection<Model.Category> NeutralCategories { get; private set; }
        public ObservableCollection<Model.Category> UnproductiveCategories { get; private set; }
        #endregion

        #region Commands
        public IAsyncCommand FloatActionCommand { get; set; }
        public IAsyncCommand<Model.Category> AddSubCategoryCommand { get; set; }
        public IAsyncCommand<Model.Category> UpdateSubCategoryCommand { get; set; }
        #endregion

        public CategoriesViewModel(Lazy<IGetAllCategoriesUseCase> useCase, Lazy<INavigationService> navigationService) : base(navigationService)
        {
            this.useCase = useCase;

            FloatActionCommand = new AsyncCommand(FloatActionCommandExecute, allowsMultipleExecutions: false);
            AddSubCategoryCommand = new AsyncCommand<Model.Category>(AddSubCategoryCommandExecute, allowsMultipleExecutions: false);
            UpdateSubCategoryCommand = new AsyncCommand<Model.Category>(UpdateSubCategoryCommandExecute, allowsMultipleExecutions: false);
        }

        private async Task FloatActionCommandExecute()
        {
            await _navigationService.NavigateAsync(nameof(FloatActionCategoriesModal), useModalNavigation: true);
        }
        private async Task AddSubCategoryCommandExecute(Model.Category category)
        {
            TrackEvent("CategoriesPage", "AddSubcategoryPage", EventFlag.Navigation);
            var navParameters = new NavigationParameters
            {
                { "SubCategory", new Model.Category { Type = category.Type } },
                { "Category", category }
            };

            await _navigationService.NavigateAsync(nameof(AddUpdateSubcategoryPage), navParameters);
        }
        private async Task UpdateSubCategoryCommandExecute(Model.Category category)
        {
            TrackEvent("CategoriesPage", "UpdateSubcategoryPage", EventFlag.Navigation);
            var navParameters = new NavigationParameters
            {
                { "SubCategory", category },
                { "Category", _categories.First(c => c.Childrens.Any(k => k.Id == category.Id)) }
            };

            await _navigationService.NavigateAsync(nameof(AddUpdateSubcategoryPage), navParameters);
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            try
            {
                var response = await _useCase.Execute();

                _categories = response.Productive.ToList();
                _categories.AddRange(response.Neutral);
                _categories.AddRange(response.Unproductive);

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

        public void OnNavigatedFrom(INavigationParameters parameters) { /* We dont need this method, but it's necessary from interface */ }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Created"))
            {
                IList<Model.Category> categoriesCreated = parameters.GetValue<IList<Model.Category>>("Created");
                InsertNewCategoriesCreated(categoriesCreated);
            }
            
            if (parameters.ContainsKey("Updated"))
            {
                IList<Model.Category> categoriesUpdated = parameters.GetValue<IList<Model.Category>>("Updated");
                UpdateCategories(categoriesUpdated);
            }
            if (parameters.ContainsKey("Deleted"))
            {
                IList<Model.Category> categoriesDeleted = parameters.GetValue<IList<Model.Category>>("Deleted");
                UpdateCategories(categoriesDeleted, true);
            }

            if (parameters.ContainsKey("UpdateList"))
            {
                var type = parameters.GetValue<CategoryType>("UpdateList");
                RaiseCategoryListCategories(type);
            }
        }

        private void RaiseCategoryListCategories(CategoryType type)
        {
            switch (type)
            {
                case CategoryType.Productive:
                    {
                        ProductiveCategories = new ObservableCollection<Model.Category>(ProductiveCategories.ToList());
                        RaisePropertyChanged("ProductiveCategories");
                    }
                    break;
                case CategoryType.Neutral:
                    {
                        NeutralCategories = new ObservableCollection<Model.Category>(NeutralCategories.ToList());
                        RaisePropertyChanged("NeutralCategories");
                    }
                    break;
                case CategoryType.Unproductive:
                    {
                        UnproductiveCategories = new ObservableCollection<Model.Category>(UnproductiveCategories.ToList());
                        RaisePropertyChanged("UnproductiveCategories");
                    }
                    break;
            }
        }
        private void InsertNewCategoriesCreated(IList<Model.Category> categoriesCreated)
        {
            switch (categoriesCreated.First().Type)
            {
                case CategoryType.Productive:
                    {
                        var newList = NewListToInsert(ProductiveCategories, categoriesCreated);

                        ProductiveCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        RaisePropertyChanged("ProductiveCategories");
                    }
                    break;
                case CategoryType.Neutral:
                    {
                        var newList = NewListToInsert(NeutralCategories, categoriesCreated);

                        NeutralCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        RaisePropertyChanged("NeutralCategories");
                    }
                    break;
                case CategoryType.Unproductive:
                    {
                        var newList = NewListToInsert(UnproductiveCategories, categoriesCreated);

                        UnproductiveCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        RaisePropertyChanged("UnproductiveCategories");
                    }
                    break;
            }
        }
        private void UpdateCategories(IList<Model.Category> categoriesUpdated, bool delete = false)
        {
            foreach(var category in categoriesUpdated)
                UpdateCategory(category, delete);

            RaisePropertyChanged("ProductiveCategories");
            RaisePropertyChanged("NeutralCategories");
            RaisePropertyChanged("UnproductiveCategories");
        }

        private void UpdateCategory(Model.Category category, bool delete = false)
        {
            switch (category.Type)
            {
                case CategoryType.Productive:
                    {
                        var newList = NewListToUpdate(ProductiveCategories, category, delete);

                        ProductiveCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                    }
                    break;
                case CategoryType.Neutral:
                    {
                        var newList = NewListToUpdate(NeutralCategories, category, delete);

                        NeutralCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                    }
                    break;
                case CategoryType.Unproductive:
                    {
                        var newList = NewListToUpdate(UnproductiveCategories, category, delete);

                        UnproductiveCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                    }
                    break;
            }
        }

        private List<Model.Category> NewListToInsert(ObservableCollection<Model.Category> categories, IList<Model.Category> categoriesCreated)
        {
            List<Model.Category> newList = categories.ToList();
            newList.AddRange(categoriesCreated);

            return newList;
        }
        private List<Model.Category> NewListToUpdate(ObservableCollection<Model.Category> categories, Model.Category categoryUpdated, bool delete = false)
        {
            List<Model.Category> newList = categories.Where(c => c.Id != categoryUpdated.Id).ToList();
            if(!delete)
                newList.Add(categoryUpdated);

            return newList;
        }
    }
}
