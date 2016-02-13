using InsuranceV2.Common.MVVM;
using Prism.Modularity;
using Prism.Regions;
using Settings.Views;

namespace Settings
{
    public class SettingsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public SettingsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof (SettingsView));
        }
    }
}