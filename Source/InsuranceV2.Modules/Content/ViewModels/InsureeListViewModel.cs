using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Application.Services;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;
using Prism.Commands;
using Prism.Common;
using Prism.Regions;

namespace InsuranceV2.Modules.Content.ViewModels
{
    public class InsureeListViewModel : DisposableViewModel
    {
        private readonly IInsureeManagementAppService _insureeManagementAppService;
        private readonly ILogger<InsureeListViewModel> _logger;
        private readonly INavigationAppService _navigationAppService;

        private ListInsuree _selectedInsuree;

        public InsureeListViewModel(IInsureeManagementAppService insureeManagementAppService,
            ILogger<InsureeListViewModel> logger, INavigationAppService navigationAppService)
        {
            _insureeManagementAppService = insureeManagementAppService;
            _logger = logger;
            _navigationAppService = navigationAppService;

            InsureeData = new ObservableCollection<ListInsuree>();
            SelectedPage = new ObservableObject<int> {Value = 1};
            PageSize = new ObservableObject<int> {Value = 5};
            TotalPages = new ObservableObject<int>();

            SubscribeEvents();

            ShowDetailsCommand = new DelegateCommand(ShowDetailsExecute, HasSelectedInsuree);
            EditInsureeCommand = new DelegateCommand(EditInsureeExecute, HasSelectedInsuree);
        }

        public ICommand ShowDetailsCommand { get; }
        public ICommand EditInsureeCommand { get; }

        public ObservableCollection<ListInsuree> InsureeData { get; }

        public ListInsuree SelectedInsuree
        {
            get { return _selectedInsuree; }
            set
            {
                _logger.Debug(value != null ? $"Selecting Insuree with Id: {value.Id}" : "Deselecting Insuree.");
                SetProperty(ref _selectedInsuree, value);
            }
        }

        public ObservableObject<int> SelectedPage { get; }

        public ObservableObject<int> TotalPages { get; }

        public ObservableObject<int> PageSize { get; }

        private void RefreshInsureeList(object sender, PropertyChangedEventArgs args)
        {
            _logger.Debug("Property changed: Refreshing list.");
            var pagedInsurees = _insureeManagementAppService.GetPagedInsurees(SelectedPage.Value, PageSize.Value);

            UpdateInsureeData(pagedInsurees.Data);
            TotalPages.Value = pagedInsurees.TotalPages;
        }

        private void ShowDetailsExecute()
        {
            _logger.Debug("Executing ShowDetailsCommand");
            var parameters = new NavigationParameters {{"SelectedInsuree", SelectedInsuree}};
            _navigationAppService.NavigateTo(ContentNames.InsureeDetailsView, parameters);
        }

        private void EditInsureeExecute()
        {
            _logger.Debug("Executing EditInsureeCommand");
            var parameters = new NavigationParameters {{"SelectedInsuree", SelectedInsuree}};
            _navigationAppService.NavigateTo(ContentNames.InsureeAddOrEditView, parameters);
        }

        private bool HasSelectedInsuree()
        {
            return SelectedInsuree != null;
        }

        private void UpdateInsureeData(IEnumerable<ListInsuree> insureeData)
        {
            InsureeData.Clear();
            InsureeData.AddRange(insureeData);
        }

        protected override void OnActivate()
        {
            _logger.Debug("Activating InsureeListView.");
            RefreshInsureeList(this, null);
        }

        protected override void OnDeactivate()
        {
            _logger.Debug("Deactivating InsureeListView.");
        }

        #region EventHandling

        private void SubscribeEvents()
        {
            SelectedPage.PropertyChanged += RefreshInsureeList;
            PageSize.PropertyChanged += RefreshInsureeList;
        }

        private void UnsubscribeEvents()
        {
            SelectedPage.PropertyChanged -= RefreshInsureeList;
            PageSize.PropertyChanged -= RefreshInsureeList;
        }

        protected override void DisposeUnmanaged()
        {
            if (SelectedPage != null && PageSize != null)
            {
                UnsubscribeEvents();
            }
        }

        #endregion
    }
}