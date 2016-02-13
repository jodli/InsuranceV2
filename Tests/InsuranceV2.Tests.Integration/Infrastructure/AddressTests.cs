using System.Linq;
using FluentAssertions;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;
using InsuranceV2.Infrastructure.Database;
using InsuranceV2.Infrastructure.Database.Context;
using InsuranceV2.Infrastructure.Repositories;
using NUnit.Framework;

namespace InsuranceV2.Tests.Integration.Infrastructure
{
    [TestFixture]
    public class AddressTests : IntegrationTestBase
    {
        public static Address CreateAddress(ContactType contactType)
        {
            return new Address("TestStreet", "123", "12345", "TestCity", "TestCountry", contactType);
        }

        [Test]
        public void AddressTypeRoundtripsToDatabase()
        {
            var newInsureeId = 0;
            var insuree = SimpleInsureeTests.CreateInsuree();
            var address = CreateAddress(ContactType.Personal);
            insuree.HomeAddress = address;
            address = CreateAddress(ContactType.Business);
            insuree.WorkAddress = address;

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }
            newInsureeId = insuree.Id;

            newInsureeId.Should().BeGreaterThan(0);

            var context = new InsuranceAppContext();
            var check = context.Insurees.First(x => x.Id == newInsureeId);
            check.Id.Should().Be(newInsureeId);
            check.HomeAddress.ContactType.Should().Be(ContactType.Personal);
            check.WorkAddress.ContactType.Should().Be(ContactType.Business);
        }
    }
}