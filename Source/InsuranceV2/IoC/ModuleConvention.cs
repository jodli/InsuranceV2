using System;
using System.Linq;
using Prism.Modularity;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace InsuranceV2.IoC
{
    public class ModuleConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.GetInterfaces().Contains(typeof (IModule)))
            {
                registry.AddType(typeof (IModule), type);
            }
        }
    }
}