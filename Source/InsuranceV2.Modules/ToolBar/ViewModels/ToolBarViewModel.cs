using System;
using System.Windows.Input;
using InsuranceV2.Application.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace ToolBar.ViewModels
{
    public class ToolBarViewModel : BindableBase
    {
        private readonly INavigationAppService _navigationAppService;

        public ToolBarViewModel(INavigationAppService navigationAppService)
        {
            _navigationAppService = navigationAppService;

            NavigateCommand = new DelegateCommand<string>(NavigateExecute);
        }

        private void NavigateExecute(string uri)
        {
            _navigationAppService.NavigateTo(new Uri(uri, UriKind.Relative));
        }

        public ICommand NavigateCommand { get; }
    }
}