using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Application.Services;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;
using Prism.Common;
using Prism.Regions;

namespace InsuranceV2.Modules.Content.ViewModels
{
    public class InsureeAddOrEditViewModel : DisposableViewModel, INavigationAware
    {
        private readonly IInsureeManagementAppService _insureeManagementAppService;
        private readonly ILogger<InsureeAddOrEditViewModel> _logger;

        public InsureeAddOrEditViewModel(ILogger<InsureeAddOrEditViewModel> logger, IEventBus eventBus,
            IInsureeManagementAppService insureeManagementAppService)
        {
            _logger = logger;
            _insureeManagementAppService = insureeManagementAppService;

            Insuree = new ObservableObject<AddOrEditInsuree>();
        }

        public ObservableObject<AddOrEditInsuree> Insuree { get; set; }

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
                Insuree.Value = _insureeManagementAppService.GetExistingInsureeToEdit(selectedInsuree.Id);
            }
            else
            {
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