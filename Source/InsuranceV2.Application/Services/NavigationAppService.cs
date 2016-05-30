using System;
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

        public void NavigateTo(Uri uri)
        {
            _logger.Info($"Navigating the {RegionNames.ContentRegion} to {uri}.");
            _regionManager.RequestNavigate(RegionNames.ContentRegion, uri, NavigationCompleted);
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