using AutoMapper;
using InsuranceV2.Application.Models.Address;
using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Application.Models.PhoneNumber;

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
                    configuration.CreateMap<Common.Models.PhoneNumber, DetailPhoneNumber>();
                });
            mapperConfiguration.AssertConfigurationIsValid();

            return mapperConfiguration.CreateMapper();
        }
    }
}