using Prism.Mvvm;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.Views.Modal.MenuOptions
{
    public class FloatActionCategoriesViewModel : BindableBase
    {
        public IAsyncCommand ProductiveCategoryCommand { get; private set; }
        public IAsyncCommand NeutralCategoryCommand { get; private set; }
        public IAsyncCommand UnproductiveCategoryCommand { get; private set; }
        public IAsyncCommand UpdateCategoryCommand { get; private set; }

        public FloatActionCategoriesViewModel()
        {
            ProductiveCategoryCommand = new AsyncCommand(null);
            NeutralCategoryCommand = new AsyncCommand(null);
            UnproductiveCategoryCommand = new AsyncCommand(null);
            UpdateCategoryCommand = new AsyncCommand(null);
        }
    }
}
