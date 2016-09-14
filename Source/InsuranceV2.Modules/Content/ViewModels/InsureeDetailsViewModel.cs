using System.Windows.Input;
using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Application.Models.Insuree.Events;
using InsuranceV2.Application.Services;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;
using Prism.Commands;
using Prism.Common;
using Prism.Events;
using Prism.Regions;

namespace InsuranceV2.Modules.Content.ViewModels
{
    public class InsureeDetailsViewModel : DisposableViewModel, INavigationAware
    {
        private readonly IInsureeManagementAppService _insureeManagementAppService;
        private readonly ILogger<InsureeDetailsViewModel> _logger;
        private readonly IEventAggregator _eventAggregator;

        private ObservableObject<bool> _isAddressExpanded;
        private ObservableObject<bool> _isEmailAddressExpanded;
        private ObservableObject<bool> _isPhoneNumberExpanded;

        public InsureeDetailsViewModel(
            IInsureeManagementAppService insureeManagementAppService,
            ILogger<InsureeDetailsViewModel> logger,
            IEventAggregator eventAggregator)
        {
            _insureeManagementAppService = insureeManagementAppService;
            _logger = logger;
            _eventAggregator = eventAggregator;

            Insuree = new ObservableObject<DetailInsuree>();

            _isAddressExpanded = new ObservableObject<bool> {Value = false};
            _isPhoneNumberExpanded = new ObservableObject<bool> {Value = false};
            _isEmailAddressExpanded = new ObservableObject<bool> {Value = false};

            ShowPartnerDetailsCommand = new DelegateCommand(ShowPartnerDetailsExecute);
        }

        public ICommand ShowPartnerDetailsCommand { get; }

        public ObservableObject<DetailInsuree> Insuree { get; set; }

        public ObservableObject<bool> IsAddressExpanded
        {
            get { return _isAddressExpanded; }
            set { SetProperty(ref _isAddressExpanded, value); }
        }

        public ObservableObject<bool> IsPhoneNumberExpanded
        {
            get { return _isPhoneNumberExpanded; }
            set { SetProperty(ref _isPhoneNumberExpanded, value); }
        }

        public ObservableObject<bool> IsEmailAddressExpanded
        {
            get { return _isEmailAddressExpanded; }
            set { SetProperty(ref _isEmailAddressExpanded, value); }
        }

        private void ShowPartnerDetailsExecute()
        {
            _logger.Debug("Executing ShowPartnerDetailsCommand");
            Insuree.Value = Insuree.Value.Partner;
            _eventAggregator.GetEvent<InsureeSelectedEvent>().Publish(Insuree);
            OnActivate();
        }

        protected override void OnActivate()
        {
            _logger.Debug("Activating InsureeDetailsView.");
            IsAddressExpanded.Value = false;
            IsPhoneNumberExpanded.Value = false;
            IsEmailAddressExpanded.Value = false;
        }

        protected override void OnDeactivate()
        {
            _logger.Debug("Deactivating InsureeDetailsView.");
        }

        #region INavigationAware

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var selectedInsuree = navigationContext.Parameters["SelectedInsuree"] as ListInsuree;
            if (selectedInsuree != null)
            {
                _logger.Debug($"Getting details for insuree with id: {selectedInsuree.Id}.");
                Insuree.Value = _insureeManagementAppService.GetDetailInsuree(selectedInsuree.Id);
                _eventAggregator.GetEvent<InsureeSelectedEvent>().Publish(Insuree);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return navigationContext.Parameters["SelectedInsuree"] != null;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        #endregion
    }
}