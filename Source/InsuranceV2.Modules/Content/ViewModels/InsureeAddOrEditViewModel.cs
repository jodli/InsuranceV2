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
    public class InsureeAddOrEditViewModel : DisposableViewModel, INavigationAware
    {
        private readonly IInsureeManagementAppService _insureeManagementAppService;
        private readonly INavigationAppService _navigationAppService;
        private readonly ILogger<InsureeAddOrEditViewModel> _logger;

        public InsureeAddOrEditViewModel(ILogger<InsureeAddOrEditViewModel> logger,
            IInsureeManagementAppService insureeManagementAppService, INavigationAppService navigationAppService)
        {
            _logger = logger;
            _insureeManagementAppService = insureeManagementAppService;
            _navigationAppService = navigationAppService;

            Insuree = new ObservableObject<AddOrEditInsuree>();

            SaveInsureeCommand = new DelegateCommand(SaveInsureeExecute);
        }

        public ObservableObject<AddOrEditInsuree> Insuree { get; set; }

        public ICommand SaveInsureeCommand { get; }

        private void SaveInsureeExecute()
        {
            _logger.Debug("Executing SaveInsureeCommand");
            if (IsExistingInsuree())
            {
                _logger.Debug($"Saving edited insuree with id: {Insuree.Value.Id}.");
                _insureeManagementAppService.EditInsuree(Insuree.Value);
            }
            else
            {
                _logger.Debug("Saving new insuree.");
                _insureeManagementAppService.AddInsuree(Insuree.Value);
            }
            _navigationAppService.NavigateTo("InsureeListView");
        }

        private bool IsExistingInsuree()
        {
            return Insuree.Value.Id > 0;
        }

        protected override void OnActivate()
        {
            _logger.Debug("Activating InsureeAddOrEditView.");
        }

        protected override void OnDeactivate()
        {
            _logger.Debug("Deactivating InsureeAddOrEditView.");
        }

        #region INavigationAware

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var selectedInsuree = navigationContext.Parameters["SelectedInsuree"] as ListInsuree;
            if (selectedInsuree != null)
            {
                _logger.Debug($"Getting insuree with id: {selectedInsuree.Id}.");
                Insuree.Value = _insureeManagementAppService.GetExistingInsureeToEdit(selectedInsuree.Id);
            }
            else
            {
                _logger.Debug("Creating a new insuree.");
                Insuree.Value = _insureeManagementAppService.GetNewInsuree();
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        #endregion
    }
}