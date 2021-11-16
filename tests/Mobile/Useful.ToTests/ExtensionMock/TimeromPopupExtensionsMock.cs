using Prism.Navigation;
using System.Threading.Tasks;

namespace Useful.ToTests.ExtensionMock
{
    public static class TimeromPopupExtensionsMock
    {
        public static Task<INavigationResult> ClearPopupStackAsync(this INavigationService navigationService, string key, object param, bool animated = true)
        {
            return Task.FromResult<INavigationResult>(new NavigationResult
            {
                Success = true
            });
        }
        public static Task<INavigationResult> ClearPopupStackAsync(this INavigationService navigationService, INavigationParameters parameters = null, bool animated = true)
        {
            return Task.FromResult<INavigationResult>(new NavigationResult
            {
                Success = true
            });
        }
    }
}
