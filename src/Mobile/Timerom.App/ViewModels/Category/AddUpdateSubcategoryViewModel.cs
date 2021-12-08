using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Timerom.App.ValueObjects.Enuns;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class AddUpdateSubcategoryViewModel : ViewModelBase, INavigationAware
    {
        #region UseCases
        private readonly Lazy<IDeleteSubcategoryUseCase> deleteUseCase;
        private readonly Lazy<IUpdateSubcategoryUseCase> updateUseCase;
        private readonly Lazy<IInsertSubcategoryUseCase> createUseCase;
        private IInsertSubcategoryUseCase _createUseCase => createUseCase.Value;
        private IUpdateSubcategoryUseCase _updateUseCase => updateUseCase.Value;
        private IDeleteSubcategoryUseCase _deleteUseCase => deleteUseCase.Value;
        #endregion

        #region Model
        private bool _updated { get; set; }
        public Model.Category Category { get; set; }
        public Model.Category SubCategory { get; set; }
        #endregion

        #region Commands
        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand DeleteCommand { get; private set; }
        #endregion

        public AddUpdateSubcategoryViewModel(Lazy<INavigationService> navigationService,
            Lazy<IInsertSubcategoryUseCase> createUseCase, Lazy<IUpdateSubcategoryUseCase> updateUseCase,
            Lazy<IDeleteSubcategoryUseCase> deleteUseCase) : base(navigationService)
        {
            this.createUseCase = createUseCase;
            this.updateUseCase = updateUseCase;
            this.deleteUseCase = deleteUseCase;

            SaveCommand = new AsyncCommand(SaveCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
            DeleteCommand = new AsyncCommand(DeleteCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
        }

        private async Task SaveCommandExecuted()
        {
            SavingStatus();

            if (SubCategory.Id == 0)
                await CreateSubCategory();
            else
                await UpdateSubCategory();

            _updated = true;
        }

        private async Task CreateSubCategory()
        {
            TrackEvent("AddUpdateSubCategoryPage", "Create", EventFlag.Click);
            var result = await _createUseCase.Execute(SubCategory, Category.Id);
            Category.Childrens.Add(result);
            Category.Childrens = new ObservableCollection<Model.Category>(Category.Childrens.OrderBy(c => c.Name));
            
            SubCategory = new Model.Category { Type = Category.Type };
            
            RaisePropertyChanged("SubCategory");

            await SucessStatus(2500);
        }
        private async Task UpdateSubCategory()
        {
            TrackEvent("AddUpdateSubCategoryPage", "Update", EventFlag.Click);
            await _updateUseCase.Execute(SubCategory, Category.Id);

            var subcategory = Category.Childrens.First(c => c.Id == SubCategory.Id);
            subcategory.Name = SubCategory.Name;

            await SucessStatus();
        }

        private async Task DeleteCommandExecuted()
        {
            NavigationParameters navParameters = new NavigationParameters
            {
                { "Title", ResourceText.TITLE_DELETE_SUBCATEGORY },
                { "Description", string.Format(ResourceText.DESCRIPTION_DELETE_SUBCATEGORY, SubCategory.Name) },
                { "Action", new AsyncCommand(DeleteSubcategory, allowsMultipleExecutions: false, onException: HandleException) }
            };

            await _navigationService.NavigateAsync(nameof(Views.Modal.ConfirmActionModal), navParameters, useModalNavigation: true);
        }

        private async Task DeleteSubcategory()
        {
            TrackEvent("AddUpdateSubCategoryPage", "Delete", EventFlag.Click);
            SavingStatus();
            await _deleteUseCase.Execute(SubCategory);

            var subcategory = Category.Childrens.First(c => c.Id == SubCategory.Id);
            Category.Childrens.Remove(subcategory);

            _updated = true;

            await _navigationService.GoBackAsync();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var category = parameters.GetValue<Model.Category>("SubCategory");
            SubCategory = new Model.Category
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type
            };

            Category = parameters.GetValue<Model.Category>("Category");

            RaisePropertyChanged("Category");
            RaisePropertyChanged("SubCategory");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            if (_updated)
                parameters.Add("UpdateList", Category.Type);
        }
    }
}
