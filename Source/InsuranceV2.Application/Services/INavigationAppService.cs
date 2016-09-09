using Prism.Regions;

namespace InsuranceV2.Application.Services
{
    public interface INavigationAppService
    {
        void GoForward();
        void GoBackward();
        void NavigateTo(string uri);
        void NavigateTo(string uri, NavigationParameters parameters);
    }
}