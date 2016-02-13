using System;
using FluentAssertions;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;
using InsuranceV2.Infrastructure.Database;
using InsuranceV2.Infrastructure.Repositories;
using NUnit.Framework;

namespace InsuranceV2.Tests.Integration.Infrastructure
{
    [TestFixture]
    internal class SimpleInsureeTests : IntegrationTestBase
    {
        public static Insuree CreateInsuree()
        {
            return new Insuree
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                DateOfBirth = DateTime.Now.AddYears(-20),
                HomeAddress = AddressTests.CreateAddress(ContactType.Personal),
                WorkAddress = AddressTests.CreateAddress(ContactType.Business)
            };
        }

        [Test]
        public void CanGetBasicInsuree()
        {
            var insuree = CreateInsuree();
            var repository = new InsureeRepository();
            using (new UnitOfWorkFactory().Create())
            {
                repository.Add(insuree);
            }
            var repositoryConfirm = new InsureeRepository();
            var check = repositoryConfirm.FindById(insuree.Id);
            check.Id.Should().Be(insuree.Id);
        }
    }
}