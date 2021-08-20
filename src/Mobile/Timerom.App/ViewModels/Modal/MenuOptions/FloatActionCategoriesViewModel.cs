using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Views.Category;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Modal.MenuOptions
{
    public class FloatActionCategoriesViewModel : BindableBase
    {
        private readonly Lazy<INavigationService> navigationService;
        private INavigationService _navigationService => navigationService.Value;

        public IAsyncCommand<CategoryType> AddCategoryCommand { get; private set; }

        public FloatActionCategoriesViewModel(Lazy<INavigationService> navigationService)
        {
            this.navigationService = navigationService;

            AddCategoryCommand = new AsyncCommand<CategoryType>(AddCategoryCommandExecuted);
        }

        private async Task AddCategoryCommandExecuted(CategoryType categoryType)
        {
            var navParameters = new NavigationParameters
            {
                { "Category", new Model.Category { Type = categoryType } }
            };

            await _navigationService.NavigateAsync(nameof(AddUpdateCategoryPage), navParameters);
        }
    }
}
