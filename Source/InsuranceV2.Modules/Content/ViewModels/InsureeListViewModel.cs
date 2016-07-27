using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableObject<int> _pageSize;
        private ListInsuree _selectedInsuree;

        private ObservableObject<int> _selectedPage;
        private ObservableObject<int> _totalPages;

        public InsureeListViewModel(IInsureeManagementAppService insureeManagementAppService,
            ILogger<InsureeListViewModel> logger, INavigationAppService navigationAppService)
        {
            _insureeManagementAppService = insureeManagementAppService;
            _logger = logger;
            _navigationAppService = navigationAppService;

            InsureeData = new ObservableCollection<ListInsuree>();
            _selectedPage = new ObservableObject<int> {Value = 1};
            _pageSize = new ObservableObject<int> {Value = 5};
            _totalPages = new ObservableObject<int>();

            UpdateListCommand = new DelegateCommand(UpdateListExecute);
            ShowDetailsCommand = new DelegateCommand(ShowDetailsExecute, HasSelectedInsuree);
            EditInsureeCommand = new DelegateCommand(EditInsureeExecute, HasSelectedInsuree);

            UpdateListExecute();
        }

        public ICommand UpdateListCommand { get; }
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

        public ObservableObject<int> SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                SetProperty(ref _selectedPage, value);
                UpdateListExecute();
            }
        }

        public ObservableObject<int> TotalPages
        {
            get { return _totalPages; }
            set { SetProperty(ref _totalPages, value); }
        }

        public ObservableObject<int> PageSize
        {
            get { return _pageSize; }
            set
            {
                SetProperty(ref _pageSize, value);
                UpdateListExecute();
            }
        }

        private void UpdateListExecute()
        {
            _logger.Debug("Executing UpdateListCommand.");
            var pagedInsurees = _insureeManagementAppService.GetPagedInsurees(SelectedPage.Value, PageSize.Value);

            UpdateInsureeData(pagedInsurees.Data);
            TotalPages.Value = pagedInsurees.TotalPages;
        }

        private void ShowDetailsExecute()
        {
            _logger.Debug("Executing ShowDetailsCommand");
            var parameters = new NavigationParameters {{"SelectedInsuree", SelectedInsuree}};
            _navigationAppService.NavigateTo("InsureeDetailsView", parameters);
        }

        private void EditInsureeExecute()
        {
            _logger.Debug("Executing EditInsureeCommand");
            var parameters = new NavigationParameters {{"SelectedInsuree", SelectedInsuree}};
            _navigationAppService.NavigateTo("InsureeAddOrEditView", parameters);
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
            UpdateListExecute();
        }

        protected override void OnDeactivate()
        {
            _logger.Debug("Deactivating InsureeListView.");
        }
    }
}