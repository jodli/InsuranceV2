using AutoMapper;
using InsuranceV2.Application.Models.Address;
using InsuranceV2.Application.Models.Insuree;

namespace InsuranceV2.Application.Models
{
    public static class AutoMapperConfig
    {
        public static IMapper Start()
        {
            var mapperConfiguration = new MapperConfiguration(
                configuration =>
                {
                    configuration.CreateMap<Common.Models.Insuree, ListInsuree>();
                    configuration.CreateMap<Common.Models.Insuree, DetailInsuree>();
                    configuration.CreateMap<Common.Models.Insuree, CreateOrEditInsuree>();
                    configuration.CreateMap<Common.Models.Address, DetailAddress>();
                });
            mapperConfiguration.AssertConfigurationIsValid();

            return mapperConfiguration.CreateMapper();
        }
    }
}