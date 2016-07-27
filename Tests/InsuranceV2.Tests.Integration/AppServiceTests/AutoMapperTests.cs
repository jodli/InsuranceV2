using System;
using System.Linq;
using FluentAssertions;
using InsuranceV2.Application.Models;
using InsuranceV2.Application.Models.Address;
using InsuranceV2.Application.Models.EmailAddress;
using InsuranceV2.Application.Models.Insurance;
using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Application.Models.PhoneNumber;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;
using NUnit.Framework;

namespace InsuranceV2.Tests.Integration.AppServiceTests
{
    [TestFixture]
    public class AutoMapperTests : AppServiceTestBase
    {
        #region Create Models

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

        private static PhoneNumber CreatePhoneNumber()
        {
            return new PhoneNumber
            {
                Number = "05728583729",
                PhoneType = PhoneType.Phone,
                ContactType = ContactType.Personal
            };
        }

        private static EmailAddress CreateEmailAddress()
        {
            return new EmailAddress
            {
                EmailAddressText = "asdf@asdf.asdf",
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

        private static AddOrEditInsuree CreateAddOrEditInsuree()
        {
            return new AddOrEditInsuree
            {
                Id = 1,
                FirstName = "editedFirstName",
                LastName = "editedLastName"
            };
        }

        private static Insurance CreateInsurance()
        {
            return new Insurance
            {
                InsuranceNumber = "ABC 1234",
                StartDate = DateTime.Now.AddDays(1)
            };
        }

        private static Insuree CreatePartner()
        {
            return new Insuree
            {
                Id = 2,
                FirstName = "partnerFirstName",
                LastName = "partnerLastName"
            };
        }

        #endregion

        [Test]
        public void AllMappingIsValid()
        {
            AutoMapperConfig.Start();
        }

        [Test]
        public void MappingInsureeToAddOrEditInsureeIsValid()
        {
            var insuree = CreateInsuree();
            var createOrEditInsuree = new AddOrEditInsuree();

            Mapper.Map(insuree, createOrEditInsuree);

            createOrEditInsuree.Id.ShouldBeEquivalentTo(insuree.Id);
            createOrEditInsuree.FirstName.ShouldBeEquivalentTo(insuree.FirstName);
            createOrEditInsuree.LastName.ShouldBeEquivalentTo(insuree.LastName);
            createOrEditInsuree.DateOfBirth.ShouldBeEquivalentTo(insuree.DateOfBirth);
        }

        [Test]
        public void MappingAddOrEditInsureeToNewInsureeIsValid()
        {
            var addOrEditInsuree = CreateAddOrEditInsuree();
            var insuree = new Insuree();

            Mapper.Map(addOrEditInsuree, insuree);

            insuree.Id.ShouldBeEquivalentTo(addOrEditInsuree.Id);
            insuree.FirstName.ShouldBeEquivalentTo(addOrEditInsuree.FirstName);
            insuree.LastName.ShouldBeEquivalentTo(addOrEditInsuree.LastName);
            insuree.DateOfBirth.ShouldBeEquivalentTo(addOrEditInsuree.DateOfBirth);
        }

        [Test]
        public void MappingAddOrEditInsureeToExistingInsureeIsValid()
        {
            var addOrEditInsuree = CreateAddOrEditInsuree();
            var insuree = CreateInsuree();

            Mapper.Map(addOrEditInsuree, insuree);

            insuree.Id.ShouldBeEquivalentTo(addOrEditInsuree.Id);

            insuree.FirstName.ShouldBeEquivalentTo(addOrEditInsuree.FirstName);
            insuree.FirstName.Should().NotBe("firstName");

            insuree.LastName.ShouldBeEquivalentTo(addOrEditInsuree.LastName);
            insuree.FirstName.Should().NotBe("lastName");

            insuree.DateOfBirth.ShouldBeEquivalentTo(insuree.DateOfBirth);
        }

        [Test]
        public void MappingInsureeToDetailInsureeIsValid()
        {
            var insuree = CreateInsuree();
            insuree.Addresses.Add(CreateAddress());
            insuree.PhoneNumbers.Add(CreatePhoneNumber());
            insuree.Partner = CreatePartner();
            insuree.Insurances.Add(CreateInsurance());
            var detailInsuree = new DetailInsuree();

            Mapper.Map(insuree, detailInsuree);

            detailInsuree.Id.ShouldBeEquivalentTo(insuree.Id);
            detailInsuree.FirstName.ShouldBeEquivalentTo(insuree.FirstName);
            detailInsuree.LastName.ShouldBeEquivalentTo(insuree.LastName);

            detailInsuree.Addresses.Count().ShouldBeEquivalentTo(insuree.Addresses.Count);
            detailInsuree.PhoneNumbers.Count().ShouldBeEquivalentTo(insuree.PhoneNumbers.Count);
            detailInsuree.Insurances.Count().ShouldBeEquivalentTo(insuree.Insurances.Count);

            detailInsuree.Partner.Id.ShouldBeEquivalentTo(insuree.Partner.Id);
            detailInsuree.Partner.FirstName.ShouldBeEquivalentTo(insuree.Partner.FirstName);
            detailInsuree.Partner.LastName.ShouldBeEquivalentTo(insuree.Partner.LastName);
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

        [Test]
        public void MappingPhoneNumberToDetailPhoneNumberIsValid()
        {
            var phoneNumber = CreatePhoneNumber();
            var detailPhoneNumber = new DetailPhoneNumber();

            Mapper.Map(phoneNumber, detailPhoneNumber);

            detailPhoneNumber.Id.ShouldBeEquivalentTo(phoneNumber.Id);
            detailPhoneNumber.Number.ShouldBeEquivalentTo(phoneNumber.Number);
            detailPhoneNumber.PhoneType.ShouldBeEquivalentTo(phoneNumber.PhoneType);
            detailPhoneNumber.ContactType.ShouldBeEquivalentTo(phoneNumber.ContactType);
        }

        [Test]
        public void MappingEmailAddressToDetailEmailAddressIsValid()
        {
            var emailAddress = CreateEmailAddress();
            var detailEmailAddress = new DetailEmailAddress();

            Mapper.Map(emailAddress, detailEmailAddress);

            detailEmailAddress.Id.ShouldBeEquivalentTo(emailAddress.Id);
            detailEmailAddress.EmailAddressText.ShouldBeEquivalentTo(emailAddress.EmailAddressText);
            detailEmailAddress.ContactType.ShouldBeEquivalentTo(emailAddress.ContactType);
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
        public void MappingInsuranceToDetailInsuranceIsValid()
        {
            var insurance = CreateInsurance();
            var detailInsurance = new DetailInsurance();

            Mapper.Map(insurance, detailInsurance);
            detailInsurance.Id.ShouldBeEquivalentTo(insurance.Id);
            detailInsurance.InsuranceNumber.ShouldBeEquivalentTo(insurance.InsuranceNumber);
            detailInsurance.StartDate.ShouldBeEquivalentTo(insurance.StartDate);
        }
    }
}