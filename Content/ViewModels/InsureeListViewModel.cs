using System.Collections.ObjectModel;
using System.Windows.Input;
using InsuranceV2.Application.Models;
using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Application.Services;
using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;

namespace Content.ViewModels
{
    public class InsureeListViewModel : BindableBase
    {
        private readonly IInsureeManagementAppService _insureeManagementAppService;
        private ObservableObject<int> _listPage;

        public InsureeListViewModel(IInsureeManagementAppService insureeManagementAppService)
        {
            _insureeManagementAppService = insureeManagementAppService;

            InsureeData = new ObservableCollection<ListInsuree>
            {
                new ListInsuree {FirstName = "firstName", FullName = "fullName", Id = 123, LastName = "LastName"}
            };
            _listPage = new ObservableObject<int> {Value = 1};

            UpdateListCommand = new DelegateCommand(UpdateListExecute);
        }

        public ICommand UpdateListCommand { get; }

        public ObservableCollection<ListInsuree> InsureeData { get; set; }

        public ObservableObject<int> ListPage
        {
            get { return _listPage; }
            set { SetProperty(ref _listPage, value); }
        }

        private void UpdateListExecute()
        {
            var pagedInsurees = _insureeManagementAppService.GetPagedInsurees(ListPage.Value);
            UpdateInsureeData(pagedInsurees);
        }

        private void UpdateInsureeData(PagerModel<ListInsuree> pagedInsurees)
        {
            InsureeData.Clear();
            InsureeData.AddRange(pagedInsurees.Data);
        }
    }
}