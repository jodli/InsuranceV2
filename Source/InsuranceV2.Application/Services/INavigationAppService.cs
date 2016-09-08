using Prism.Regions;

namespace InsuranceV2.Application.Services
{
    public interface INavigationAppService
    {
        void NavigateToPrevious();
        void NavigateToNext();
        void NavigateTo(string uri);
        void NavigateTo(string uri, NavigationParameters parameters);
    }
}