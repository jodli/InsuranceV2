using System.Linq;
using FluentAssertions;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;
using NUnit.Framework;

namespace InsuranceV2.Tests.Unit
{
    [TestFixture]
    public class AddressTests : UnitTestBase
    {
        [Test]
        public void AddressWithValuesIsNotNull()
        {
            var address = new Address
            {
                Street = "street",
                StreetNumber = "123",
                ZipCode = "12345",
                City = "city",
                Country = "country",
                ContactType = ContactType.Personal
            };
            address.IsNull.Should().BeFalse();
        }

        [Test]
        public void EmptyAddressIsNull()
        {
            var address = new Address
            {
                Street = null,
                StreetNumber = null,
                ZipCode = null,
                City = null,
                Country = null,
                ContactType = ContactType.Personal
            };
            address.IsNull.Should().BeTrue();
        }

        [Test]
        public void EmptyAddressIsValid()
        {
            var address = new Address
            {
                Street = null,
                StreetNumber = null,
                ZipCode = null,
                City = null,
                Country = null,
                ContactType = ContactType.Personal
            };
            address.Validate().Count().Should().Be(0);
        }

        [Test]
        public void PartialAddressIsInvalid()
        {
            var address = new Address
            {
                Street = null,
                StreetNumber = null,
                ZipCode = null,
                City = null,
                Country = "country",
                ContactType = ContactType.Personal
            };
            address.Validate().Count().Should().NotBe(0);
            address.Validate().Should().NotContain(x => x.MemberNames.Contains("Country"));
        }
    }
}