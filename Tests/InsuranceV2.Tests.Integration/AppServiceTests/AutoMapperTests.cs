using System.Linq;
using FluentAssertions;
using InsuranceV2.Application.Models;
using InsuranceV2.Application.Models.Address;
using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;
using NUnit.Framework;

namespace InsuranceV2.Tests.Integration.AppServiceTests
{
    [TestFixture]
    public class AutoMapperTests : AppServiceTestBase
    {
        private static Address CreateAddress()
        {
            return new Address
            {
                Street = "street",
                StreetNumber = "123",
                ZipCode = "12345",
                City = "city",
                Country = "country",
                ContactType = ContactType.Personal
            };
        }

        private static Insuree CreateInsuree()
        {
            return new Insuree
            {
                Id = 1,
                FirstName = "firstName",
                LastName = "lastName"
            };
        }

        [Test]
        public void AllMappingIsValid()
        {
            AutoMapperConfig.Start();
        }

        [Test]
        public void MappingAddressToDetailAddressIsValid()
        {
            var address = CreateAddress();
            var detailAddress = new DetailAddress();

            Mapper.Map(address, detailAddress);

            detailAddress.Id.ShouldBeEquivalentTo(address.Id);
            detailAddress.Street.ShouldBeEquivalentTo(address.Street);
            detailAddress.StreetNumber.ShouldBeEquivalentTo(address.StreetNumber);
            detailAddress.ZipCode.ShouldBeEquivalentTo(address.ZipCode);
            detailAddress.City.ShouldBeEquivalentTo(address.City);
            detailAddress.Country.ShouldBeEquivalentTo(address.Country);
            detailAddress.ContactType.ShouldBeEquivalentTo(address.ContactType);
        }

        [Test]
        public void MappingInsureeToCreateOrEditInsureeIsValid()
        {
            var insuree = CreateInsuree();
            var createOrEditInsuree = new CreateOrEditInsuree();

            Mapper.Map(insuree, createOrEditInsuree);

            createOrEditInsuree.Id.ShouldBeEquivalentTo(insuree.Id);
            createOrEditInsuree.FirstName.ShouldBeEquivalentTo(insuree.FirstName);
            createOrEditInsuree.LastName.ShouldBeEquivalentTo(insuree.LastName);
            createOrEditInsuree.DateOfBirth.ShouldBeEquivalentTo(insuree.DateOfBirth);
        }

        [Test]
        public void MappingInsureeToDetailInsureeIsValid()
        {
            var insuree = CreateInsuree();
            insuree.Addresses.Add(CreateAddress());
            var detailInsuree = new DetailInsuree();

            Mapper.Map(insuree, detailInsuree);

            detailInsuree.Id.ShouldBeEquivalentTo(insuree.Id);
            detailInsuree.FirstName.ShouldBeEquivalentTo(insuree.FirstName);
            detailInsuree.LastName.ShouldBeEquivalentTo(insuree.LastName);

            detailInsuree.Addresses.Count().ShouldBeEquivalentTo(insuree.Addresses.Count());
        }

        [Test]
        public void MappingInsureeToListInsureeIsValid()
        {
            var insuree = CreateInsuree();
            var listInsuree = new ListInsuree();

            Mapper.Map(insuree, listInsuree);

            listInsuree.Id.ShouldBeEquivalentTo(insuree.Id);
            listInsuree.FirstName.ShouldBeEquivalentTo(insuree.FirstName);
            listInsuree.LastName.ShouldBeEquivalentTo(insuree.LastName);
        }
    }
}