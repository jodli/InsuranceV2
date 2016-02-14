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

            TestLogging();
        }

        private static void TestLogging()
        {
            var logger = new Logger<CommonRegistry>();
            logger.Debug("Testing Debug Log.");
            logger.Info("Testing Info Log.");
            logger.Warn("Testing Warn Log.");
            logger.Error("Testing Error Log.");
        }
    }
}