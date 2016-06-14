using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace InsuranceV2.Domain.DependencyResolution
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}