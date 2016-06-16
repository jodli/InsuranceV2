using InsuranceV2.Common.MVVM;
using InsuranceV2.Modules.Main.Views;
using Prism.Modularity;
using Prism.Regions;

namespace InsuranceV2.Modules.Main
{
    public class MainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (MainView));
        }
    }
}