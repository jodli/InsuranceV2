using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Application.Services;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;

namespace Content.ViewModels
{
    public class InsureeDetailsViewModel : DisposableViewModel
    {
        private readonly IEventBus _eventBus;
        private readonly IInsureeManagementAppService _insureeManagementAppService;
        private readonly ILogger<InsureeDetailsViewModel> _logger;

        public InsureeDetailsViewModel(IInsureeManagementAppService insureeManagementAppService,
            ILogger<InsureeDetailsViewModel> logger, IEventBus eventBus)
        {
            _insureeManagementAppService = insureeManagementAppService;
            _logger = logger;
            _eventBus = eventBus;

            SubscribeEvents();
        }

        public DetailInsuree Insuree { get; set; }

        private void SelectedInsureeChanged(ListInsuree listInsuree)
        {
            _logger.Debug(listInsuree != null
                ? $"Getting DetailInsuree with Id: {listInsuree.Id}."
                : "Unselected Insuree.");
            if (listInsuree != null)
            {
                Insuree = _insureeManagementAppService.GetDetailInsuree(listInsuree.Id);
            }
        }

        #region EventBus

        private void SubscribeEvents()
        {
            _eventBus.Subscribe<ListInsuree>(SelectedInsureeChanged);
        }

        private void UnsubscribeEvents()
        {
            _eventBus.Unsubscribe<ListInsuree>(SelectedInsureeChanged);
        }

        protected override void DisposeUnmanaged()
        {
            if (_eventBus != null)
            {
                UnsubscribeEvents();
            }
        }

        #endregion
    }
}