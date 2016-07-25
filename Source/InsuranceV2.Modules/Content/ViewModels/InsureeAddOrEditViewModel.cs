using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;

namespace InsuranceV2.Modules.Content.ViewModels
{
    public class InsureeAddOrEditViewModel : DisposableViewModel
    {
        private readonly ILogger<InsureeAddOrEditViewModel> _logger;

        public InsureeAddOrEditViewModel(ILogger<InsureeAddOrEditViewModel> logger)
        {
            _logger = logger;
        }

        protected override void OnActivate()
        {
            _logger.Debug("Activating InsureeAddOrEditView.");
        }

        protected override void OnDeactivate()
        {
            _logger.Debug("Deactivating InsureeAddOrEditView.");
        }
    }
}