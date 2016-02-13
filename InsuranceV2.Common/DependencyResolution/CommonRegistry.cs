using InsuranceV2.Common.Logging;
using log4net.Config;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace InsuranceV2.Common.DependencyResolution
{
    public class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });

            For(typeof (ILogger<>)).Use(typeof (Logger<>));
            XmlConfigurator.Configure();
        }
    }
}