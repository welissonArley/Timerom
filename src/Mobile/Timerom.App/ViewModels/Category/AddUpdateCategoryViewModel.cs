using Prism.Mvvm;
using Prism.Navigation;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class AddUpdateCategoryViewModel : BindableBase, INavigationAware
    {
        public Model.Category Category { get; set; }
        public string SubCategoryName { get; set; }

        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand AddSubCategoryCommand { get; private set; }
        public IAsyncCommand<Model.Category> OptionCategoryCommand { get; private set; }

        public AddUpdateCategoryViewModel()
        {
            SaveCommand = new AsyncCommand(SaveCommandExecuted, allowsMultipleExecutions: false);
            AddSubCategoryCommand = new AsyncCommand(AddSubCategoryCommandExecuted, allowsMultipleExecutions: false);
            OptionCategoryCommand = new AsyncCommand<Model.Category>(OptionCategoryCommandExecuted, allowsMultipleExecutions: false);
        }

        private async Task SaveCommandExecuted()
        {

        }
        private Task AddSubCategoryCommandExecuted()
        {
            if(Category.Childrens.All(c => !c.Name.ToUpper().Equals(SubCategoryName.ToUpper())))
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

        public void OnNavigatedFrom(INavigationParameters parameters) { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Category = parameters.GetValue<Model.Category>("Category");
            RaisePropertyChanged("Category");
        }
    }
}
