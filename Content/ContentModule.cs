using Content.Views;
using InsuranceV2.Common.MVVM;
using Prism.Modularity;
using Prism.Regions;

namespace Content
{
    public class ContentModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ContentModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof (InsureeListView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof (SettingsView));
        }
    }
}