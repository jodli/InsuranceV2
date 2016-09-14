using System;
using System.Linq.Dynamic;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.Models;
using InsuranceV2.Common.MVVM;
using Prism.Regions;

namespace InsuranceV2.Application.Services
{
    public class NavigationAppService : INavigationAppService
    {
        private readonly ILogger<NavigationAppService> _logger;
        private readonly IRegionManager _regionManager;
        private readonly IEventBus _eventBus;

        private Uri _currentRegion;

        public NavigationAppService(
            IRegionManager regionManager, 
            ILogger<NavigationAppService> logger,
            IEventBus eventBus)
        {
            _regionManager = regionManager;
            _logger = logger;
            _eventBus = eventBus;

            _currentRegion = null;
        }

        public void GoForward()
        {
            var contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
            if (contentRegion.NavigationService.Journal.CanGoForward)
            {
                contentRegion.NavigationService.Journal.GoForward();
            }
            CheckNavigationPossibilites();
        }

        public void GoBackward()
        {
            var contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
            if (contentRegion.NavigationService.Journal.CanGoBack)
            {
                contentRegion.NavigationService.Journal.GoBack();
            }
            CheckNavigationPossibilites();
        }

        private void CheckNavigationPossibilites()
        {
            IRegion contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
            _eventBus.Publish(new NavigationDetails(_currentRegion.OriginalString, contentRegion.NavigationService.Journal.CanGoBack, contentRegion.NavigationService.Journal.CanGoForward));
        }

        private void NavigateTo(Uri uri)
        {
            NavigateTo(uri, new NavigationParameters());
        }

        private void NavigateTo(Uri uri, NavigationParameters parameters)
        {
            _logger.Info($"Navigating the {RegionNames.ContentRegion} to {uri} with {parameters.Count()} parameters.");
            _currentRegion = uri;
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
                    CheckNavigationPossibilites();
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