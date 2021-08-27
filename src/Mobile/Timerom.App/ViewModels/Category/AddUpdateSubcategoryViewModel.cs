using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class AddUpdateSubcategoryViewModel : ViewModelBase, INavigationAware
    {
        #region UseCases
        private readonly Lazy<IUpdateSubcategoryUseCase> updateUseCase;
        private readonly Lazy<IInsertSubcategoryUseCase> createUseCase;
        private IInsertSubcategoryUseCase _createUseCase => createUseCase.Value;
        private IUpdateSubcategoryUseCase _updateUseCase => updateUseCase.Value;
        #endregion

        #region Model
        private bool _updated { get; set; }
        public Model.Category Category { get; set; }
        public Model.Category SubCategory { get; set; }
        #endregion

        #region Commands
        public IAsyncCommand SaveCommand { get; private set; }
        #endregion

        public AddUpdateSubcategoryViewModel(Lazy<INavigationService> navigationService,
            Lazy<IInsertSubcategoryUseCase> createUseCase, Lazy<IUpdateSubcategoryUseCase> updateUseCase) : base(navigationService)
        {
            this.createUseCase = createUseCase;
            this.updateUseCase = updateUseCase;

            SaveCommand = new AsyncCommand(SaveCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
        }

        private async Task SaveCommandExecuted()
        {
            SavingStatus();

            if (Category.Id == 0)
                await CreateCategory();
            else
                await UpdateCategory();

            _updated = true;
        }

        private async Task CreateCategory()
        {
            var result = await _createUseCase.Execute(SubCategory, Category.Id);
            Category.Childrens.Add(result);
            Category.Childrens = new ObservableCollection<Model.Category>(Category.Childrens.OrderBy(c => c.Name));
            
            SubCategory = new Model.Category { Type = Category.Type };
            
            RaisePropertyChanged("SubCategory");

            await SucessStatus(2500);
        }
        private async Task UpdateCategory()
        {
            await _updateUseCase.Execute(SubCategory, Category.Id);

            var subcategory = Category.Childrens.First(c => c.Id == SubCategory.Id);
            subcategory.Name = SubCategory.Name;

            await SucessStatus();
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
