using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;

namespace InsuranceV2.Modules.Content.ViewModels
{
    public class StartupViewModel : DisposableViewModel
    {
        private readonly ILogger<StartupViewModel> _logger;

        public StartupViewModel(ILogger<StartupViewModel> logger)
        {
            _logger = logger;
        }

        protected override void OnActivate()
        {
            _logger.Debug("Activating StartupView.");
        }

        protected override void OnDeactivate()
        {
            _logger.Debug("Deactivating StartupView.");
        }
    }
}