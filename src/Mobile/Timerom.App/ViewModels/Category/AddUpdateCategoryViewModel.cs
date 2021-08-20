using Prism.Mvvm;
using Prism.Navigation;

namespace Timerom.App.ViewModels.Category
{
    public class AddUpdateCategoryViewModel : BindableBase, INavigationAware
    {
        public Model.Category Category { get; set; }

        public AddUpdateCategoryViewModel()
        {

        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Category = parameters.GetValue<Model.Category>("Category");
            RaisePropertyChanged("Category");
        }
    }
}
