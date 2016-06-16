using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace InsuranceV2.Infrastructure.DependencyResolution
{
    public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}