using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.Views.Modal.MenuOptions;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class CategoriesViewModel : ViewModelBase, IInitializeAsync, INavigationAware
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

        public void OnNavigatedFrom(INavigationParameters parameters) { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Created"))
            {
                IList<Model.Category> categoriesCreated = parameters.GetValue<IList<Model.Category>>("Created");
                InsertNewCategoriesCreated(categoriesCreated);
            }
            else if (parameters.ContainsKey("Updated"))
            {
                IList<Model.Category> categoriesUpdated = parameters.GetValue<IList<Model.Category>>("Updated");
                UpdateCategories(categoriesUpdated);
            }
        }

        private void InsertNewCategoriesCreated(IList<Model.Category> categoriesCreated)
        {
            switch (categoriesCreated.First().Type)
            {
                case ValueObjects.Enuns.CategoryType.Productive:
                    {
                        var newList = NewListToInsert(ProductiveCategories, categoriesCreated);

                        ProductiveCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        RaisePropertyChanged("ProductiveCategories");
                    }
                    break;
                case ValueObjects.Enuns.CategoryType.Neutral:
                    {
                        var newList = NewListToInsert(NeutralCategories, categoriesCreated);

                        NeutralCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        RaisePropertyChanged("NeutralCategories");
                    }
                    break;
                case ValueObjects.Enuns.CategoryType.Unproductive:
                    {
                        var newList = NewListToInsert(UnproductiveCategories, categoriesCreated);

                        UnproductiveCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        RaisePropertyChanged("UnproductiveCategories");
                    }
                    break;
            }
        }
        private void UpdateCategories(IList<Model.Category> categoriesUpdated)
        {
            foreach(var category in categoriesUpdated)
            {
                switch (category.Type)
                {
                    case ValueObjects.Enuns.CategoryType.Productive:
                        {
                            var newList = NewListToUpdate(ProductiveCategories, category);

                            ProductiveCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        }
                        break;
                    case ValueObjects.Enuns.CategoryType.Neutral:
                        {
                            var newList = NewListToUpdate(NeutralCategories, category);

                            NeutralCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        }
                        break;
                    case ValueObjects.Enuns.CategoryType.Unproductive:
                        {
                            var newList = NewListToUpdate(UnproductiveCategories, category);

                            UnproductiveCategories = new ObservableCollection<Model.Category>(newList.OrderBy(c => c.Name));
                        }
                        break;
                }
            }

            RaisePropertyChanged("ProductiveCategories");
            RaisePropertyChanged("NeutralCategories");
            RaisePropertyChanged("UnproductiveCategories");
        }

        private List<Model.Category> NewListToInsert(ObservableCollection<Model.Category> categories, IList<Model.Category> categoriesCreated)
        {
            List<Model.Category> newList = categories.ToList();
            newList.AddRange(categoriesCreated);

            return newList;
        }
        private List<Model.Category> NewListToUpdate(ObservableCollection<Model.Category> categories, Model.Category categoryUpdated)
        {
            List<Model.Category> newList = categories.Where(c => c.Id != categoryUpdated.Id).ToList();
            newList.Add(categoryUpdated);

            return newList;
        }
    }
}
