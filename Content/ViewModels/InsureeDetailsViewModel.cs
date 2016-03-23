using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using InsuranceV2.Application.Models.Address;
using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Application.Services;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;
using Prism.Commands;
using Prism.Common;

namespace Content.ViewModels
{
    public class InsureeDetailsViewModel : DisposableViewModel
    {
        private readonly IEventBus _eventBus;
        private readonly IInsureeManagementAppService _insureeManagementAppService;
        private readonly ILogger<InsureeDetailsViewModel> _logger;

        private ObservableObject<Visibility> _showAddressesVisibility;

        public InsureeDetailsViewModel(IInsureeManagementAppService insureeManagementAppService,
            ILogger<InsureeDetailsViewModel> logger, IEventBus eventBus)
        {
            _insureeManagementAppService = insureeManagementAppService;
            _logger = logger;
            _eventBus = eventBus;

            Insuree = new ObservableObject<DetailInsuree>();

            ToggleShowAddressesCommand = new DelegateCommand(ToggleShowAddressesExecute);
            _showAddressesVisibility = new ObservableObject<Visibility> {Value = Visibility.Collapsed};

            SubscribeEvents();
        }

        public ObservableObject<DetailInsuree> Insuree { get; set; }

        public ICommand ToggleShowAddressesCommand { get; }

        public ObservableObject<Visibility> ShowAddressesVisibility
        {
            get { return _showAddressesVisibility; }
            set { SetProperty(ref _showAddressesVisibility, value); }
        }

        private void ToggleShowAddressesExecute()
        {
            _logger.Debug("Executing ToggleShowAddressesCommand");
            ShowAddressesVisibility.Value = ShowAddressesVisibility.Value == Visibility.Collapsed
                ? Visibility.Visible
                : Visibility.Collapsed;
            _logger.Info($"Detailed addresses are {ShowAddressesVisibility.Value.ToString("G")}.");
        }

        private void SelectedInsureeChanged(ListInsuree listInsuree)
        {
            _logger.Debug(listInsuree != null
                ? $"Getting DetailInsuree with Id: {listInsuree.Id}."
                : "Unselected Insuree.");
            if (listInsuree != null)
            {
                Insuree.Value = _insureeManagementAppService.GetDetailInsuree(listInsuree.Id);
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