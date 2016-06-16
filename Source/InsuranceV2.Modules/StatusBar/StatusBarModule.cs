using InsuranceV2.Common.MVVM;
using InsuranceV2.Modules.StatusBar.Views;
using Prism.Modularity;
using Prism.Regions;

namespace InsuranceV2.Modules.StatusBar
{
    public class StatusBarModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public StatusBarModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.StatusBarRegion, typeof (StatusBarView));
        }
    }
}