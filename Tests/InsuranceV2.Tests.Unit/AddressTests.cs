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
            var address = new Address("Street", "StreetNumber", "City", "ZipCode", "Country", ContactType.Business);
            address.IsNull.Should().BeFalse();
        }

        [Test]
        public void EmptyAddressIsNull()
        {
            var address = new Address(null, null, null, null, null, ContactType.Personal);
            address.IsNull.Should().BeTrue();
        }

        [Test]
        public void EmptyAddressIsValid()
        {
            var address = new Address(null, null, null, null, null, ContactType.Personal);
            address.Validate().Count().Should().Be(0);
        }

        [Test]
        public void PartialAddressIsInvalid()
        {
            var address = new Address(null, null, null, null, "Country", ContactType.Personal);
            address.Validate().Should().NotContain(x => x.MemberNames.Contains("Country"));
        }

        [Test]
        public void TwoSameAddressesShouldBeSame()
        {
            var address1 = new Address("Street", "StreetNumber", "City", "ZipCode", "Country", ContactType.Personal);
            var address2 = new Address("Street", "StreetNumber", "City", "ZipCode", "Country", ContactType.Personal);
            (address1 == address2).Should().BeTrue();
        }
    }
}