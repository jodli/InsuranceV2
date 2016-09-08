using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;

namespace InsuranceV2.Modules.Content.ViewModels
{
    public class InformationViewModel : DisposableViewModel
    {
        private readonly ILogger<InformationViewModel> _logger;

        public InformationViewModel(ILogger<InformationViewModel> logger)
        {
            _logger = logger;
        }

        protected override void OnActivate()
        {
            _logger.Debug("Activating InformationView.");
        }

        protected override void OnDeactivate()
        {
            _logger.Debug("Deactivating InformationView.");
        }
    }
}