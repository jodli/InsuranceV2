using System;
using System.Linq;
using FluentAssertions;
using InsuranceV2.Common;
using InsuranceV2.Common.Collections;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;
using NUnit.Framework;

namespace InsuranceV2.Tests.Unit
{
    [TestFixture]
    public class InsureeTests : UnitTestBase
    {
        private static Insuree CreateInsuree()
        {
            return new Insuree
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
        }

        [Test]
        public void CanAddEmailAddressToNewInsuree()
        {
            var insuree = new Insuree();
            insuree.EmailAddresses.Add(new EmailAddress());
            insuree.EmailAddresses.Count.Should().Be(1);
        }

        [Test]
        public void CanAddPhoneNumbersToNewInsuree()
        {
            var insuree = new Insuree();
            insuree.PhoneNumbers.Add(new PhoneNumber());
            insuree.PhoneNumbers.Count.Should().Be(1);
        }

        [Test]
        public void CanCreateInstanceOfInsuree()
        {
            var insuree = new Insuree();
            insuree.Should().NotBeNull();
            insuree.Id.Should().Be(0);
        }

        [Test]
        public void ConstructorWithInitialListAddsToInsurees()
        {
            var initialList = new Insurees {new Insuree(), new Insuree()};
            var insurees = new Insurees(initialList);
            insurees.Count.Should().Be(2);
        }

        [Test]
        public void DetectInvalidAddress()
        {
            var insuree = CreateInsuree();
            insuree.Addresses.Add("Street", "", "", "", "", ContactType.None);
            var errors = insuree.Validate();
            errors.Should()
                .NotContain(x => x.MemberNames.Contains("Street"))
                .And.Contain(x => x.MemberNames.Contains("StreetNumber"))
                .And.Contain(x => x.MemberNames.Contains("ZipCode"))
                .And.Contain(x => x.MemberNames.Contains("City"))
                .And.Contain(x => x.MemberNames.Contains("Country"))
                .And.Contain(x => x.MemberNames.Contains("ContactType"));
        }

        [Test]
        public void DetectInvalidEmailAddressText()
        {
            var insuree = CreateInsuree();
            insuree.EmailAddresses.Add("", ContactType.Personal);
            var errors = insuree.Validate();
            errors.Should().Contain(x => x.MemberNames.Contains("EmailAddressText"));
        }

        [Test]
        public void DetectInvalidInsureeInInsurees()
        {
            var insurees = new Insurees
            {
                new Insuree {FirstName = "First"},
                new Insuree {LastName = "Last"}
            };
            var errors = insurees.Validate();
            errors.Should()
                .Contain(x => x.MemberNames.Contains("LastName"))
                .And.Contain(x => x.MemberNames.Contains("FirstName"));
        }

        [Test]
        public void DetectInvalidPhoneNumber()
        {
            var insuree = CreateInsuree();
            insuree.PhoneNumbers.Add("", PhoneType.None, ContactType.None);
            var errors = insuree.Validate();
            errors.Should()
                .Contain(x => x.MemberNames.Contains("Number"))
                .And.Contain(x => x.MemberNames.Contains("PhoneType"))
                .And.Contain(x => x.MemberNames.Contains("ContactType"));
        }

        [Test]
        public void FullNameIsFirstNameAndLastName()
        {
            var insuree = new Insuree {FirstName = "First", LastName = "Last"};
            insuree.FullName.Should().Be("Last, First");
        }

        [Test]
        public void FullNameReturnsEmpty()
        {
            var insuree = new Insuree();
            insuree.FullName.Should().Be(string.Empty);
        }

        [Test]
        public void InsureeNotTooOld()
        {
            var insuree = CreateInsuree();
            insuree.DateOfBirth = DateTime.Now.AddDays(-Constants.MaxAgeInsuree).AddDays(1);
            insuree.Validate().Count(x => x.MemberNames.Contains("DateOfBirth")).Should().Be(0);
        }

        [Test]
        public void InsureeNotTooYoung()
        {
            var insuree = CreateInsuree();
            insuree.DateOfBirth = DateTime.Now.AddDays(-1);
            insuree.Validate().Count(x => x.MemberNames.Contains("DateOfBirth")).Should().Be(0);
        }

        [Test]
        public void InsureeTooOld()
        {
            var insuree = CreateInsuree();
            insuree.DateOfBirth = DateTime.Now.AddYears(-Constants.MaxAgeInsuree).AddDays(-1);
            insuree.Validate().Count(x => x.MemberNames.Contains("DateOfBirth")).Should().BeGreaterThan(0);
            insuree.Validate().Count(x => x.ErrorMessage.Contains("Invalid range")).Should().BeGreaterThan(0);
        }

        [Test]
        public void InsureeTooYoung()
        {
            var insuree = CreateInsuree();
            insuree.DateOfBirth = DateTime.Now.AddDays(1);
            insuree.Validate().Count(x => x.MemberNames.Contains("DateOfBirth")).Should().BeGreaterThan(0);
            insuree.Validate().Count(x => x.ErrorMessage.Contains("Invalid range")).Should().BeGreaterThan(0);
        }

        [Test]
        public void NewInsureeHasHomeAddress()
        {
            var insuree = new Insuree();
            insuree.Addresses.Should().NotBeNull();
        }

        [Test]
        public void NewInsureeHasListOfEmailAddresses()
        {
            var insuree = new Insuree();
            insuree.EmailAddresses.Should().NotBeNull();
            insuree.EmailAddresses.Should().BeEmpty();
        }

        [Test]
        public void NewInsureeHasListOfPhoneNumbers()
        {
            var insuree = new Insuree();
            insuree.PhoneNumbers.Should().NotBeNull();
            insuree.PhoneNumbers.Should().BeEmpty();
        }

        [Test]
        public void NewInsureeHasListOfAddresses()
        {
            var insuree = new Insuree();
            insuree.Addresses.Should().NotBeNull();
            insuree.Addresses.Should().BeEmpty();
        }

        [Test]
        public void TwoInsureeWithSameIdAreSame()
        {
            var insuree1 = new Insuree {Id = 1, FirstName = "TestFirstName", LastName = "TestLastName"};
            var insuree2 = new Insuree {Id = 1, FirstName = "TestFirstName", LastName = "TestLastName"};
            (insuree1 == insuree2).Should().BeTrue();
        }
    }
}