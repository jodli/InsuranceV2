using InsuranceV2.Common.MVVM;
using InsuranceV2.Modules.Content.Views;
using Prism.Modularity;
using Prism.Regions;

namespace InsuranceV2.Modules.Content
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
      _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(StartupView));
      _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(InsureeListView));
      _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(InsureeDetailsView));
      _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(InsureeAddOrEditView));
      _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(SettingsView));
      _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(InformationView));
    }
  }
}