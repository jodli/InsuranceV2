using InsuranceV2.Common.MVVM;
using Prism.Modularity;
using Prism.Regions;
using ToolBar.Views;

namespace ToolBar
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