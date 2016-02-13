using System;
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
        [Test]
        public void AllMappingIsValid()
        {
            AutoMapperConfig.Start();
        }

        [Test]
        public void MappingAddressToDetailAddressIsValid()
        {
            var address = new Address("street", "123", "12345", "city", "country", ContactType.Personal);
            var detailAddress = new DetailAddress();

            Mapper.Map(address, detailAddress);

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
            var insuree = new Insuree
            {
                FirstName = "firstName",
                LastName = "lastName",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            var createOrEditInsuree = new CreateOrEditInsuree();

            Mapper.Map(insuree, createOrEditInsuree);

            createOrEditInsuree.Id.ShouldBeEquivalentTo(insuree.Id);
            createOrEditInsuree.FirstName.ShouldBeEquivalentTo(insuree.FirstName);
            createOrEditInsuree.LastName.ShouldBeEquivalentTo(insuree.LastName);
            createOrEditInsuree.DateOfBirth.ShouldBeEquivalentTo(insuree.DateOfBirth);
        }

        [Test]
        public void MappingInsureeToListInsureeIsValid()
        {
            var insuree = new Insuree
            {
                Id = 1,
                FirstName = "firstName",
                LastName = "lastName"
            };
            var listInsuree = new DisplayInsuree();

            Mapper.Map(insuree, listInsuree);

            listInsuree.Id.ShouldBeEquivalentTo(insuree.Id);
            listInsuree.FirstName.ShouldBeEquivalentTo(insuree.FirstName);
            listInsuree.LastName.ShouldBeEquivalentTo(insuree.LastName);
        }
    }
}