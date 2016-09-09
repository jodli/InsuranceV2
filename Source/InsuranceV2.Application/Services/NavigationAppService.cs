using System;
using System.Linq.Dynamic;
using InsuranceV2.Common.Events;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;
using Prism.Events;
using Prism.Regions;

namespace InsuranceV2.Application.Services
{
    public class NavigationAppService : INavigationAppService
    {
        private readonly ILogger<NavigationAppService> _logger;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private Uri _currentRegion;

        public NavigationAppService(
            IRegionManager regionManager, 
            ILogger<NavigationAppService> logger, 
            IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _logger = logger;
            _eventAggregator = eventAggregator;

            _currentRegion = null;
        }

        public void GoForward()
        {
            var contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
            if (contentRegion.NavigationService.Journal.CanGoForward)
            {
                contentRegion.NavigationService.Journal.GoForward();

                _eventAggregator.GetEvent<NavigationChangedEvent>().Publish(contentRegion.NavigationService.Journal.CurrentEntry.Uri.OriginalString);
            }
            CheckNavigationPossibilites();
        }

        public void GoBackward()
        {
            var contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
            if (contentRegion.NavigationService.Journal.CanGoBack)
            {
                contentRegion.NavigationService.Journal.GoBack();

                _eventAggregator.GetEvent<NavigationChangedEvent>().Publish(contentRegion.NavigationService.Journal.CurrentEntry.Uri.OriginalString);
            }
            CheckNavigationPossibilites();
        }

        private void CheckNavigationPossibilites()
        {
            var contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
            _eventAggregator.GetEvent<NavigationCanGoBackEvent>().Publish(contentRegion.NavigationService.Journal.CanGoBack);
            _eventAggregator.GetEvent<NavigationCanGoForwardEvent>().Publish(contentRegion.NavigationService.Journal.CanGoForward);
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
                    _eventAggregator.GetEvent<NavigationChangedEvent>().Publish(_currentRegion.OriginalString);
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