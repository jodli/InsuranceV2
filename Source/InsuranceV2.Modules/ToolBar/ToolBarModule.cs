using InsuranceV2.Common.MVVM;
using InsuranceV2.Modules.ToolBar.Views;
using Prism.Modularity;
using Prism.Regions;

namespace InsuranceV2.Modules.ToolBar
{
    public class ToolBarModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ToolBarModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof (ToolBarView));
        }
    }
}