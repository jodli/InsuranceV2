using System;
using System.Windows;
using System.Windows.Markup;
using Content;
using InsuranceV2.Application.DependencyResolution;
using InsuranceV2.Common.DependencyResolution;
using InsuranceV2.Common.Logging;
using InsuranceV2.Domain.DependencyResolution;
using InsuranceV2.Infrastructure.DependencyResolution;
using InsuranceV2.Views;
using Main;
using Prism.Logging;
using Prism.Modularity;
using Prism.StructureMap;
using StatusBar;
using StructureMap.Graph;
using ToolBar;

namespace InsuranceV2.IoC
{
    public class Bootstrapper : StructureMapBootstrapper
    {
        protected override void ConfigureContainer()
        {
            Container.Configure(container =>
            {
                container.AddRegistry<AppRegistry>();
                container.AddRegistry<ModuleRegistry>();
                container.AddRegistry<ApplicationRegistry>();
                container.AddRegistry<DomainRegistry>();
                container.AddRegistry<InfrastructureRegistry>();
                container.AddRegistry<CommonRegistry>();

                container.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });

                container.For<IComponentConnector>().OnCreationForAll(x => x.InitializeComponent());
            });
            base.ConfigureContainer();
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new Logger<DebugLogger>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            AddModuleToCatalog(typeof (MainModule));
            AddModuleToCatalog(typeof (ToolBarModule));
            AddModuleToCatalog(typeof (StatusBarModule));

            AddModuleToCatalog(typeof (ContentModule));
        }

        private void AddModuleToCatalog(Type type)
        {
            var newModuleInfo = new ModuleInfo
            {
                ModuleName = type.AssemblyQualifiedName,
                ModuleType = type.AssemblyQualifiedName
            };

            ModuleCatalog.AddModule(newModuleInfo);
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);

            InitializeShell();
        }

        protected override DependencyObject CreateShell()
        {
            Shell = Container.GetInstance<Shell>();
            return Shell;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            System.Windows.Application.Current.MainWindow = (Window) Shell;
            System.Windows.Application.Current.MainWindow.Show();
        }
    }
}