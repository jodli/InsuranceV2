using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;

namespace InsuranceV2.Modules.Content.ViewModels
{
    public class SettingsViewModel : DisposableViewModel
    {
        private readonly ILogger<SettingsViewModel> _logger;

        public SettingsViewModel(ILogger<SettingsViewModel> logger)
        {
            _logger = logger;
        }

        protected override void OnActivate()
        {
            _logger.Debug("Activating SettingsView.");
        }

        protected override void OnDeactivate()
        {
            _logger.Debug("Deactivating SettingsView.");
        }
    }
}