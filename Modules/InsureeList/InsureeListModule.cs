using InsuranceV2.Common.MVVM;
using InsureeList.Views;
using Prism.Modularity;
using Prism.Regions;

namespace InsureeList
{
    public class InsureeListModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public InsureeListModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof (InsureeListView));
        }
    }
}