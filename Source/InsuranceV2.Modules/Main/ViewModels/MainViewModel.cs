using System;
using InsuranceV2.Application.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace InsuranceV2.Modules.Main.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly INavigationAppService _navigationAppService;

        public MainViewModel(INavigationAppService navigationAppService)
        {
            _navigationAppService = navigationAppService;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        private void Navigate(string uri)
        {
            _navigationAppService.NavigateTo(new Uri(uri, UriKind.Relative));
        }
    }
}