using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Application.Services;
using InsuranceV2.Common.Logging;
using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;

namespace Content.ViewModels
{
    public class InsureeListViewModel : BindableBase
    {
        private readonly IInsureeManagementAppService _insureeManagementAppService;
        private readonly ILogger<InsureeListViewModel> _logger;
        private ObservableObject<int> _pageSize;
        private ListInsuree _selectedInsuree;

        private ObservableObject<int> _selectedPage;
        private ObservableObject<int> _totalPages;

        public InsureeListViewModel(IInsureeManagementAppService insureeManagementAppService,
            ILogger<InsureeListViewModel> logger)
        {
            _insureeManagementAppService = insureeManagementAppService;
            _logger = logger;

            InsureeData = new ObservableCollection<ListInsuree>();
            _selectedPage = new ObservableObject<int> {Value = 1};
            _pageSize = new ObservableObject<int> {Value = 5};
            _totalPages = new ObservableObject<int>();

            UpdateListCommand = new DelegateCommand(UpdateListExecute);

            UpdateListExecute();
        }

        public ICommand UpdateListCommand { get; }

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
            set { SetProperty(ref _selectedPage, value); }
        }

        public ObservableObject<int> TotalPages
        {
            get { return _totalPages; }
            set { SetProperty(ref _totalPages, value); }
        }

        public ObservableObject<int> PageSize
        {
            get { return _pageSize; }
            set { SetProperty(ref _pageSize, value); }
        }

        private void UpdateListExecute()
        {
            _logger.Debug("Executing UpdateListCommand.");
            var pagedInsurees = _insureeManagementAppService.GetPagedInsurees(SelectedPage.Value, PageSize.Value);

            UpdateInsureeData(pagedInsurees.Data);
            TotalPages.Value = pagedInsurees.TotalPages;
        }

        private void UpdateInsureeData(IEnumerable<ListInsuree> insureeData)
        {
            InsureeData.Clear();
            InsureeData.AddRange(insureeData);
        }
    }
}