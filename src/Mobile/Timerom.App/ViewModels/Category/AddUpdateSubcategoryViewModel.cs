using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Category
{
    public class AddUpdateSubcategoryViewModel : ViewModelBase, INavigationAware
    {
        public string Category { get; set; }
        public Model.Category SubCategory { get; set; }

        public IAsyncCommand SaveCommand { get; private set; }

        public AddUpdateSubcategoryViewModel()
        {
            
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

            Category = parameters.GetValue<string>("Category");

            RaisePropertyChanged("Category");
            RaisePropertyChanged("SubCategory");
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
    }
}
