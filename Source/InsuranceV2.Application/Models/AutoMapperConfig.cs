using AutoMapper;
using InsuranceV2.Application.Models.Address;
using InsuranceV2.Application.Models.EmailAddress;
using InsuranceV2.Application.Models.Insurance;
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
                    configuration.CreateMap<Common.Models.Insuree, AddOrEditInsuree>();
                    configuration.CreateMap<Common.Models.Address, DetailAddress>();
                    configuration.CreateMap<Common.Models.PhoneNumber, DetailPhoneNumber>();
                    configuration.CreateMap<Common.Models.EmailAddress, DetailEmailAddress>();
                    configuration.CreateMap<Common.Models.Insurance, ListInsurance>();
                    configuration.CreateMap<Common.Models.Insurance, DetailInsurance>();
                });
            mapperConfiguration.AssertConfigurationIsValid();

            return mapperConfiguration.CreateMapper();
        }
    }
}