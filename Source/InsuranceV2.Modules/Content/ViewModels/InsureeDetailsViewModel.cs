using System.Windows;
using System.Windows.Input;
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
        private ObservableObject<Visibility> _showEmailAddressesVisibility;
        private ObservableObject<Visibility> _showPhoneNumbersVisibility;

        public InsureeDetailsViewModel(IInsureeManagementAppService insureeManagementAppService,
            ILogger<InsureeDetailsViewModel> logger, IEventBus eventBus)
        {
            _insureeManagementAppService = insureeManagementAppService;
            _logger = logger;
            _eventBus = eventBus;

            Insuree = new ObservableObject<DetailInsuree>();

            ToggleShowAddressesCommand = new DelegateCommand(ToggleShowAddressesExecute);
            _showAddressesVisibility = new ObservableObject<Visibility> {Value = Visibility.Collapsed};

            ToggleShowPhoneNumbersCommand = new DelegateCommand(ToggleShowPhoneNumbersExecute);
            _showPhoneNumbersVisibility = new ObservableObject<Visibility> {Value = Visibility.Collapsed};

            ToggleShowEmailAddressesCommand = new DelegateCommand(ToggleShowEmailAddressesExecute);
            _showEmailAddressesVisibility = new ObservableObject<Visibility> {Value = Visibility.Collapsed};

            SubscribeEvents();
        }

        public ObservableObject<DetailInsuree> Insuree { get; set; }

        public ICommand ToggleShowAddressesCommand { get; }

        public ObservableObject<Visibility> ShowAddressesVisibility
        {
            get { return _showAddressesVisibility; }
            set { SetProperty(ref _showAddressesVisibility, value); }
        }

        public ObservableObject<Visibility> ShowPhoneNumbersVisibility
        {
            get { return _showPhoneNumbersVisibility; }
            set { SetProperty(ref _showPhoneNumbersVisibility, value); }
        }

        public ICommand ToggleShowPhoneNumbersCommand { get; }

        public ICommand ToggleShowEmailAddressesCommand { get; }

        public ObservableObject<Visibility> ShowEmailAddressesVisibility
        {
            get { return _showEmailAddressesVisibility; }
            set { SetProperty(ref _showEmailAddressesVisibility, value); }
        }

        private void ToggleShowEmailAddressesExecute()
        {
            _logger.Debug("Executing ToggleShowEmailAddressesCommand");
            ShowEmailAddressesVisibility.Value = ShowEmailAddressesVisibility.Value == Visibility.Collapsed
                ? Visibility.Visible
                : Visibility.Collapsed;
            _logger.Info($"Detailed email addresses are {ShowEmailAddressesVisibility.Value.ToString("G")}.");
        }

        private void ToggleShowAddressesExecute()
        {
            _logger.Debug("Executing ToggleShowAddressesCommand");
            ShowAddressesVisibility.Value = ShowAddressesVisibility.Value == Visibility.Collapsed
                ? Visibility.Visible
                : Visibility.Collapsed;
            _logger.Info($"Detailed addresses are {ShowAddressesVisibility.Value.ToString("G")}.");
        }

        private void ToggleShowPhoneNumbersExecute()
        {
            _logger.Debug("Executing ToggleShowPhoneNumbersExecute");
            ShowPhoneNumbersVisibility.Value = ShowPhoneNumbersVisibility.Value == Visibility.Collapsed
                ? Visibility.Visible
                : Visibility.Collapsed;
            _logger.Info($"Detailed phone numbers are {ShowPhoneNumbersVisibility.Value.ToString("G")}.");
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