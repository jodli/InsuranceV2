using System.Collections.ObjectModel;
using InsuranceV2.Common.Models;
using Prism.Mvvm;

namespace InsureeList.ViewModels
{
    public class InsureeListViewModel : BindableBase
    {
        public InsureeListViewModel()
        {
            Insurees = new ObservableCollection<Insuree>
            {
                new Insuree()
            };
        }

        public ObservableCollection<Insuree> Insurees { get; }
    }
}