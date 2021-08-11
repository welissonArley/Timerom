using Timerom.App.ValueObjects.Enuns;

namespace Timerom.App.ViewModels.Home
{
    public class HomePageFlyoutViewModel
    {
        public MenuItemOptions? MenuItemSelected { get; set; } = null;

        public HomePageFlyoutViewModel(MenuItemOptions selected)
        {
            MenuItemSelected = selected;
        }
    }
}
