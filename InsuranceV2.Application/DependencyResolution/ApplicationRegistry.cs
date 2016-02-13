using AutoMapper;
using InsuranceV2.Application.Models;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace InsuranceV2.Application.DependencyResolution
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });

            var mapperConfiguration = AutoMapperConfig.Start();
            For<IMapper>().Use(mapperConfiguration);
        }
    }
}