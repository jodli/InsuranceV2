using System.Collections.ObjectModel;
using System.Windows.Input;
using InsuranceV2.Application.Models;
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

        private ObservableObject<int> _listPage;
        private ObservableObject<int> _pageSize;
        private ListInsuree _selectedInsuree;

        public InsureeListViewModel(IInsureeManagementAppService insureeManagementAppService,
            ILogger<InsureeListViewModel> logger)
        {
            _insureeManagementAppService = insureeManagementAppService;
            _logger = logger;

            InsureeData = new ObservableCollection<ListInsuree>();
            _listPage = new ObservableObject<int> {Value = 1};
            _pageSize = new ObservableObject<int> {Value = 5};

            UpdateListCommand = new DelegateCommand(UpdateListExecute);

            UpdateListExecute();
        }

        public ICommand UpdateListCommand { get; }

        public ObservableCollection<ListInsuree> InsureeData { get; set; }

        public ListInsuree SelectedInsuree
        {
            get { return _selectedInsuree; }
            set
            {
                _logger.Debug($"Selecting Insuree with Id: {value.Id}");
                SetProperty(ref _selectedInsuree, value);
            }
        }

        public ObservableObject<int> ListPage
        {
            get { return _listPage; }
            set { SetProperty(ref _listPage, value); }
        }

        public ObservableObject<int> PageSize
        {
            get { return _pageSize; }
            set { SetProperty(ref _pageSize, value); }
        }

        private void UpdateListExecute()
        {
            _logger.Debug("Executing UpdateListCommand.");
            var pagedInsurees = _insureeManagementAppService.GetPagedInsurees(ListPage.Value, PageSize.Value);
            UpdateInsureeData(pagedInsurees);
        }

        private void UpdateInsureeData(PagerModel<ListInsuree> pagedInsurees)
        {
            InsureeData.Clear();
            InsureeData.AddRange(pagedInsurees.Data);
        }
    }
}