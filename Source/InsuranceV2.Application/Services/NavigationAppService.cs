using System;
using System.Linq.Dynamic;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;
using Prism.Regions;

namespace InsuranceV2.Application.Services
{
    public class NavigationAppService : INavigationAppService
    {
        private readonly ILogger<NavigationAppService> _logger;
        private readonly IRegionManager _regionManager;

        public NavigationAppService(IRegionManager regionManager, ILogger<NavigationAppService> logger)
        {
            _regionManager = regionManager;
            _logger = logger;
        }

        private void NavigateTo(Uri uri)
        {
            NavigateTo(uri, new NavigationParameters());
        }

        private void NavigateTo(Uri uri, NavigationParameters parameters)
        {
            _logger.Info($"Navigating the {RegionNames.ContentRegion} to {uri} with {parameters.Count()} parameters.");
            _regionManager.RequestNavigate(RegionNames.ContentRegion, uri, NavigationCompleted, parameters);
        }

        public void NavigateTo(string uri)
        {
            NavigateTo(new Uri(uri, UriKind.Relative));
        }

        public void NavigateTo(string uri, NavigationParameters parameters)
        {
            NavigateTo(new Uri(uri, UriKind.Relative), parameters);
        }

        private void NavigationCompleted(NavigationResult navigationResult)
        {
            if (navigationResult.Result != null)
            {
                if (navigationResult.Result.Value)
                {
                    _logger.Info("Navigation successful.");
                }
                else
                {
                    _logger.Error("Navigation failed.", navigationResult.Error);
                }
            }
        }
    }
}