using System;
using System.ComponentModel;
using System.Windows;
using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.Models;
using InsuranceV2.Common.MVVM;
using Prism.Common;

namespace InsuranceV2.Modules.StatusBar.ViewModels
{
    public class StatusBarViewModel : DisposableViewModel, INotifyPropertyChanged
    {
        private readonly ILogger<StatusBarViewModel> _logger;
        private readonly IEventBus _eventBus;

        private Visibility _statusBarVisibility;
        private Visibility _employeeVisibility;
        private Visibility _clientVisibility;

        private string _employeeName;
        private string _titleName;
        private string _clientName;

        public StatusBarViewModel(ILogger<StatusBarViewModel> logger, IEventBus eventBus)
        {
            _logger = logger;
            _logger.Debug("StatusBarViewModel created!");

            _eventBus = eventBus;

            _statusBarVisibility = Visibility.Collapsed;
            _employeeVisibility = Visibility.Collapsed;
            _clientVisibility = Visibility.Collapsed;

            _titleName = "Title";
            _employeeName = "Employee!";
            _clientName = "Client!";

            SubscribeEvents();
        }

        protected override void OnActivate()
        {
            _logger.Debug("Activating StatusBarViewModel.");
        }

        protected override void OnDeactivate()
        {
            _logger.Debug("Deactivating StatusBarViewModel.");
        }

        #region Visibility

        public Visibility StatusBarVisibility
        {
            get { return _statusBarVisibility; }
            set
            {
                _statusBarVisibility = value;
                RaisePropertyChanged("StatusBarVisibility");
            }
        }

        public Visibility EmployeeVisibility
        {
            get { return _employeeVisibility; }
            set
            {
                _employeeVisibility = value;
                RaisePropertyChanged("EmployeeVisibility");

            }
        }

        public Visibility ClientVisibility
        {
            get { return _clientVisibility; }
            set
            {
                _clientVisibility = value;
                RaisePropertyChanged("ClientVisibility");
            }
        }

        #endregion

        #region Text

        public string TitleName
        {
            get { return _titleName; }
            set
            {
                _titleName = value;
                RaisePropertyChanged("TitleName");
            }
        }

        public string EmployeeName
        {
            get { return _employeeName; }
            set
            {
                _employeeName = value;
                RaisePropertyChanged("EmployeeName");
            }
        }

        public string ClientName
        {
            get { return _clientName; }
            set
            {
                _clientName = value;
                RaisePropertyChanged("ClientName");
            }
        }

        #endregion

        #region Events

        public new event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SubscribeEvents()
        {
            _eventBus.Subscribe<ObservableObject<DetailInsuree>>(OnInsureeChanged);
            _eventBus.Subscribe<NavigationDetails>(OnNavigationChanged);
        }

        private void OnNavigationChanged(NavigationDetails navigationDetails)
        {
            switch (navigationDetails.CurrentRegion)
            {
                case ContentNames.StartupView:
                    {
                        StatusBarVisibility = Visibility.Collapsed;

                        ClientName = "";

                        break;
                    }
                case ContentNames.InsureeListView:
                    {
                        StatusBarVisibility = Visibility.Visible;

                        EmployeeVisibility = Visibility.Visible;
                        ClientVisibility = Visibility.Hidden;

                        ClientName = "";

                        TitleName = "Versicherte";
                        EmployeeName = "Test Tester";

                        break;
                    }
                case ContentNames.InsureeDetailsView:
                    {
                        StatusBarVisibility = Visibility.Visible;

                        EmployeeVisibility = Visibility.Visible;
                        ClientVisibility = Visibility.Collapsed;

                        TitleName = ClientName;
                        EmployeeName = "Test Tester";

                        break;
                    }
                case ContentNames.InsureeAddOrEditView:
                    {
                        StatusBarVisibility = Visibility.Visible;

                        EmployeeVisibility = Visibility.Visible;
                        ClientVisibility = Visibility.Visible;

                        TitleName = "Change or add something";
                        EmployeeName = "Test Tester";

                        break;
                    }
                case ContentNames.SettingsView:
                    {
                        StatusBarVisibility = Visibility.Visible;

                        EmployeeVisibility = Visibility.Collapsed;
                        ClientVisibility = Visibility.Collapsed;

                        TitleName = "Settings";

                        break;
                    }
                case ContentNames.InformationView:
                    {
                        StatusBarVisibility = Visibility.Visible;

                        EmployeeVisibility = Visibility.Collapsed;
                        ClientVisibility = Visibility.Collapsed;

                        TitleName = "Informations";

                        break;
                    }
                default:
                    {
                        throw new NotSupportedException(String.Format("Region {0} not supported!", navigationDetails.CurrentRegion));
                    }
            }
        }

        private void OnInsureeChanged(ObservableObject<DetailInsuree> selectedInsuree)
        {
            ClientName = selectedInsuree.Value.FullName;
            TitleName = ClientName;
        }
        
        private void UnSubscribeEvents()
        {
            _eventBus.Unsubscribe<ObservableObject<DetailInsuree>>(OnInsureeChanged);
            _eventBus.Unsubscribe<NavigationDetails>(OnNavigationChanged);
        }

        protected override void DisposeUnmanaged()
        {
            if (_eventBus != null)
            {
                UnSubscribeEvents();
            }
        }

        #endregion
    }
}