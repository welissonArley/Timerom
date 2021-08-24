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
        private readonly Lazy<IInsertCategoryUseCase> useCase;
        protected IInsertCategoryUseCase _useCase => useCase.Value;

        private IList<Model.Category> _categoriesCreated { get; set; }
        public Model.Category Category { get; set; }
        public string SubCategoryName { get; set; }

        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand AddSubCategoryCommand { get; private set; }
        public IAsyncCommand<Model.Category> OptionCategoryCommand { get; private set; }

        public AddUpdateCategoryViewModel(Lazy<INavigationService> navigationService, Lazy<IInsertCategoryUseCase> useCase) : base(navigationService)
        {
            this.useCase = useCase;

            _categoriesCreated = new List<Model.Category>();

            SaveCommand = new AsyncCommand(SaveCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
            AddSubCategoryCommand = new AsyncCommand(AddSubCategoryCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
            OptionCategoryCommand = new AsyncCommand<Model.Category>(OptionCategoryCommandExecuted, allowsMultipleExecutions: false, onException: HandleException);
        }

        private async Task SaveCommandExecuted()
        {
            SavingStatus();

            var result = await _useCase.Execute(Category);

            _categoriesCreated.Add(result);

            SubCategoryName = "";
            Category = new Model.Category { Type = Category.Type };
            RaisePropertyChanged("SubCategoryName");
            RaisePropertyChanged("Category");

            await SucessStatus(2500);
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
