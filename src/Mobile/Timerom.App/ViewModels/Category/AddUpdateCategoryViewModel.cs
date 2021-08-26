using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Interfaces;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class AddUpdateCategoryViewModel : ViewModelBase, INavigationAware
    {
        private readonly Lazy<IUpdateCategoryUseCase> updateUseCase;
        private readonly Lazy<IInsertCategoryUseCase> createUseCase;
        protected IInsertCategoryUseCase _createUseCase => createUseCase.Value;
        protected IUpdateCategoryUseCase _updateUseCase => updateUseCase.Value;

        private IList<Model.Category> _categoriesCreated { get; set; }
        public Model.Category Category { get; set; }
        public string SubCategoryName { get; set; }

        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand AddSubCategoryCommand { get; private set; }
        public IAsyncCommand<Model.Category> OptionCategoryCommand { get; private set; }

        public AddUpdateCategoryViewModel(Lazy<INavigationService> navigationService,
            Lazy<IInsertCategoryUseCase> createUseCase, Lazy<IUpdateCategoryUseCase> updateUseCase) : base(navigationService)
        {
            this.createUseCase = createUseCase;
            this.updateUseCase = updateUseCase;

            _categoriesCreated = new List<Model.Category>();

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

        private async Task CreateCategory()
        {
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
            await _updateUseCase.Execute(Category);
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
        private Task OptionCategoryCommandExecuted(Model.Category category)
        {
            Category.Childrens.Remove(category);
            RaisePropertyChanged("Category.Childrens");

            return Task.CompletedTask;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            if(_categoriesCreated.Any())
                parameters.Add("Created", _categoriesCreated);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Category = parameters.GetValue<Model.Category>("Category");
            RaisePropertyChanged("Category");
        }
    }
}
