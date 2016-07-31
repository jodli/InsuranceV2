using System;
using System.Windows.Input;
using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Application.Services;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.MVVM;
using Prism.Commands;
using Prism.Common;
using Prism.Interactivity.InteractionRequest;
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
            ConfirmationRequest = new InteractionRequest<IConfirmation>();

            Insuree = new ObservableObject<AddOrEditInsuree>();

            SaveAddOrEditInsureeCommand = new DelegateCommand(SaveAddOrEditInsureeExecute, CanSaveAddOrEditInsuree);
            CancelAddOrEditInsureeCommand = new DelegateCommand(CancelAddOrEditInsureeExecute);
        }

        public ObservableObject<AddOrEditInsuree> Insuree { get; set; }

        public ICommand SaveAddOrEditInsureeCommand { get; }
        public ICommand CancelAddOrEditInsureeCommand { get; }

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; }

        private void SaveAddOrEditInsureeExecute()
        {
            _logger.Debug("Executing SaveAddOrEditInsureeCommand");
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
            _navigationAppService.NavigateTo(ContentNames.InsureeListView);
        }

        private bool CanSaveAddOrEditInsuree()
        {
            return true;
        }

        private void CancelAddOrEditInsureeExecute()
        {
            _logger.Debug("Executing CancelAddOrEditInsureeCommand");
            ConfirmationRequest.Raise(new Confirmation
            {
                Title = "Änderungen verwerfen?",
                Content = "Möchten Sie die Änderungen wirklich verwerfen?"
            }, NavigateToInsureeListView);
        }

        private void NavigateToInsureeListView(IConfirmation confirmation)
        {
            _logger.Debug($"Cancel AddOrEditInsuree confirmed: {confirmation.Confirmed}");
            if (confirmation.Confirmed)
            {
                _navigationAppService.NavigateTo(ContentNames.InsureeListView);
            }
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