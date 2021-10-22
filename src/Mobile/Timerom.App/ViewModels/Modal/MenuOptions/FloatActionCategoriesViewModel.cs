using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.Views.Views.Category;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Modal.MenuOptions
{
    public class FloatActionCategoriesViewModel : ViewModelBase
    {
        public IAsyncCommand<CategoryType> AddCategoryCommand { get; private set; }
        public IAsyncCommand UpdateCategoryCommand { get; private set; }

        public FloatActionCategoriesViewModel(Lazy<INavigationService> navigationService) : base(navigationService)
        {
            AddCategoryCommand = new AsyncCommand<CategoryType>(AddCategoryCommandExecuted, allowsMultipleExecutions: false);
            UpdateCategoryCommand = new AsyncCommand(UpdateCategoryCommandExecuted, allowsMultipleExecutions: false);
        }

        private async Task AddCategoryCommandExecuted(CategoryType categoryType)
        {
            var navParameters = new NavigationParameters
            {
                { "Category", new Model.Category { Type = categoryType } }
            };

            await _navigationService.NavigateAsync(nameof(AddUpdateCategoryPage), navParameters);
        }
        private async Task UpdateCategoryCommandExecuted()
        {
            await _navigationService.NavigateAsync(nameof(SelectCategoryToUpdatePage));
        }
    }
}
