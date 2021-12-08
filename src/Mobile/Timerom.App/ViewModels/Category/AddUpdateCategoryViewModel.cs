using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class AddUpdateCategoryViewModel : ViewModelBase, INavigationAware
    {
        #region UseCases
        private readonly Lazy<IDeleteCategoryUseCase> deleteUseCase;
        private readonly Lazy<IUpdateCategoryUseCase> updateUseCase;
        private readonly Lazy<IInsertCategoryUseCase> createUseCase;
        private readonly Lazy<ICanDeleteSubcategoryUseCase> canDeleteSubcategoryUse;
        protected IInsertCategoryUseCase _createUseCase => createUseCase.Value;
        protected IUpdateCategoryUseCase _updateUseCase => updateUseCase.Value;
        protected IDeleteCategoryUseCase _deleteUseCase => deleteUseCase.Value;
        protected ICanDeleteSubcategoryUseCase _canDeleteSubcategoryUse => canDeleteSubcategoryUse.Value;
        #endregion

        #region Model
        private IList<Model.Category> _categoriesCreated { get; set; }
        private Model.Category _categoriesDeleted { get; set; }
        private Model.Category _updateCategory { get; set; }
        public Model.Category Category { get; set; }
        public string SubCategoryName { get; set; }
        #endregion

        #region Commands
        public IAsyncCommand DeleteCommand { get; private set; }
        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand AddSubCategoryCommand { get; private set; }
        public IAsyncCommand<Model.Category> OptionCategoryCommand { get; private set; }
        #endregion

        public AddUpdateCategoryViewModel(Lazy<INavigationService> navigationService,
            Lazy<IInsertCategoryUseCase> createUseCase, Lazy<IUpdateCategoryUseCase> updateUseCase,
            Lazy<IDeleteCategoryUseCase> deleteUseCase, Lazy<ICanDeleteSubcategoryUseCase> canDeleteSubcategoryUse) : base(navigationService)
        {
            this.createUseCase = createUseCase;
            this.updateUseCase = updateUseCase;
            this.deleteUseCase = deleteUseCase;
            this.canDeleteSubcategoryUse = canDeleteSubcategoryUse;

            _categoriesCreated = new List<Model.Category>();

            DeleteCommand = new AsyncCommand(DeleteCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
            SaveCommand = new AsyncCommand(SaveCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
            AddSubCategoryCommand = new AsyncCommand(AddSubCategoryCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
            OptionCategoryCommand = new AsyncCommand<Model.Category>(OptionCategoryCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
        }

        private async Task SaveCommandExecuted()
        {
            SavingStatus();

            if (Category.Id == 0)
                await CreateCategory();
            else
                await UpdateCategory();
        }

        private async Task DeleteCommandExecuted()
        {
            NavigationParameters navParameters = new NavigationParameters
            {
                { "Title", ResourceText.TITLE_DELETE_CATEGORY },
                { "Description", string.Format(ResourceText.DESCRIPTION_DELETE_CATEGORY, Category.Name) },
                { "Action", new AsyncCommand(DeleteCategory, allowsMultipleExecutions: false, onException: HandleException) }
            };

            await _navigationService.NavigateAsync(nameof(Views.Modal.ConfirmActionModal), navParameters, useModalNavigation: true);
        }

        private async Task DeleteCategory()
        {
            TrackEvent("AddUpdateCategoryPage", "Delete", EventFlag.Click);
            SavingStatus();
            await _deleteUseCase.Execute(Category);

            _categoriesDeleted = Category;
            await _navigationService.GoBackAsync();
        }

        private async Task CreateCategory()
        {
            TrackEvent("AddUpdateCategoryPage", "Create", EventFlag.Click);
            var result = await _createUseCase.Execute(Category);

            _categoriesCreated.Add(result);

            SubCategoryName = "";
            Category = new Model.Category { Type = Category.Type };
            RaisePropertyChanged("SubCategoryName");
            RaisePropertyChanged("Category");

            await SucessStatus(2500);
        }
        private async Task UpdateCategory()
        {
            TrackEvent("AddUpdateCategoryPage", "Update", EventFlag.Click);
            _updateCategory = await _updateUseCase.Execute(Category);
            await SucessStatus();
        }

        private Task AddSubCategoryCommandExecuted()
        {
            if(!string.IsNullOrWhiteSpace(SubCategoryName) &&
                Category.Childrens.All(c => !c.Name.ToUpper().Equals(SubCategoryName.ToUpper())))
            {
                Category.Childrens.Add(new Model.Category
                {
                    Name = SubCategoryName,
                    Type = Category.Type
                });

                RaisePropertyChanged("Category.Childrens");
            }

            SubCategoryName = "";
            RaisePropertyChanged("SubCategoryName");

            return Task.CompletedTask;
        }
        private async Task OptionCategoryCommandExecuted(Model.Category category)
        {
            await _canDeleteSubcategoryUse.Execute(category);

            Category.Childrens.Remove(category);
            RaisePropertyChanged("Category.Childrens");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            if(_categoriesCreated.Any())
                parameters.Add("Created", _categoriesCreated);
            if (_updateCategory != null)
                parameters.Add("Updated", _updateCategory);
            if (_categoriesDeleted != null)
                parameters.Add("Deleted", _categoriesDeleted);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var category = parameters.GetValue<Model.Category>("Category");
            Category = new Model.Category
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type,
                Childrens = new ObservableCollection<Model.Category>(category.Childrens.Select(c => new Model.Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type
                }))
            };
            RaisePropertyChanged("Category");
        }
    }
}
