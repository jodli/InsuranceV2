using System;
using FluentAssertions;
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
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
        }

        [Test]
        public void CanGetBasicInsuree()
        {
            var insuree = CreateInsuree();
            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                var check = repository.FindById(insuree.Id);
                check.Id.Should().Be(insuree.Id);
            }
        }
    }
}