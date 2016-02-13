using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace InsuranceV2.IoC
{
    public class AppRegistry : Registry
    {
        public AppRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}