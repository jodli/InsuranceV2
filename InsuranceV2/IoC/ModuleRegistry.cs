using Prism.Modularity;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace InsuranceV2.IoC
{
    public class ModuleRegistry : Registry
    {
        public ModuleRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.Convention<ModuleConvention>();
            });
            ForSingletonOf<IModuleManager>().Use<ModuleManager>();
        }
    }
}