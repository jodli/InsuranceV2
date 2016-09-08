using System;
using System.Collections.Generic;
using System.Linq.Dynamic;
using InsuranceV2.Common.Events;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;
using Prism.Events;
using Prism.Regions;

namespace InsuranceV2.Application.Services
{
    //TODO: create list of navigationEntries to navigate to next and previous!

    public class NavigationAppService : INavigationAppService
    {
        private readonly ILogger<NavigationAppService> _logger;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private Uri _currentNavigation;
        private List<Uri> _navigationHistory;
        private int _historyIndex;

        public NavigationAppService(IRegionManager regionManager, ILogger<NavigationAppService> logger, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _logger = logger;
            _eventAggregator = eventAggregator;

            _currentNavigation = null;
            _navigationHistory = new List<Uri>();
            _historyIndex = 0;
        }

        private void NavigateTo(Uri uri)
        {
            NavigateTo(uri, new NavigationParameters());
        }

        private void NavigateTo(Uri uri, NavigationParameters parameters)
        {
            _logger.Info($"Navigating the {RegionNames.ContentRegion} to {uri} with {parameters.Count()} parameters.");
            _currentNavigation = uri;
            _regionManager.RequestNavigate(RegionNames.ContentRegion, uri, NavigationCompleted, parameters);
        }

        public void NavigateToPrevious()
        {
            _historyIndex--;
            if (_historyIndex >= 0)
            {
                throw new NotImplementedException();
            }
        }

        public void NavigateToNext()
        {
            _historyIndex++;
            if (_historyIndex <= _navigationHistory.Count() - 1)
            {
                throw new NotImplementedException();
            }
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
                    _navigationHistory.Add(_currentNavigation);
                    _historyIndex = _navigationHistory.Count() - 1;
                    _eventAggregator.GetEvent<NavigationChangedEvent>().Publish(_currentNavigation.OriginalString);
                    _logger.Info("Navigation successful.");
                }
                else
                {
                    _logger.Error("Navigation failed.", navigationResult.Error);
                }
                _currentNavigation = null;
            }
        }
    }
}