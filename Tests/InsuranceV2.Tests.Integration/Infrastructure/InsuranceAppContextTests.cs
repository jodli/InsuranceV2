using System;
using System.Linq;
using FluentAssertions;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Exceptions;
using InsuranceV2.Common.Models;
using InsuranceV2.Infrastructure.Database;
using InsuranceV2.Infrastructure.Database.Context;
using InsuranceV2.Infrastructure.Repositories;
using NUnit.Framework;

namespace InsuranceV2.Tests.Integration.Infrastructure
{
    [TestFixture]
    public class InsuranceAppContextTests : IntegrationTestBase
    {
        [Test]
        public void CanAddInsureeUsingInsuranceAppContext()
        {
            var insuree = new Insuree
            {
                DateOfBirth = DateTime.Now,
                DateCreated = DateTime.Now,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                DateModified = DateTime.Now,
                HomeAddress = AddressTests.CreateAddress(ContactType.Personal)
            };

            var context = new InsuranceAppContext();
            context.Insurees.Add(insuree);
            context.SaveChanges();

            insuree.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void CanCreateInsuranceAppContext()
        {
            var context = new InsuranceAppContext();
        }

        [Test]
        public void CanExecuteQueryAgainstDataContext()
        {
            var lastName = Guid.NewGuid().ToString();
            var context = new InsuranceAppContext();

            var insuree = new Insuree
            {
                DateOfBirth = DateTime.Now,
                DateCreated = DateTime.Now,
                FirstName = "TestFirstName",
                LastName = lastName,
                DateModified = DateTime.Now,
                HomeAddress = AddressTests.CreateAddress(ContactType.Personal)
            };

            context.Insurees.Add(insuree);
            context.SaveChanges();

            var insureeCheck = context.Insurees.Single(x => x.LastName == lastName);
            insureeCheck.Should().NotBeNull();
        }

        [Test]
        public void ValidationErrorThrowsModelValidationException()
        {
            var unitOfWork = new UnitOfWorkFactory().Create();
            Action act = () =>
            {
                var repository = new InsureeRepository();
                repository.Add(new Insuree());
                unitOfWork.Commit(true);
            };
            act.ShouldThrow<ModelValidationException>();
            unitOfWork.Undo();
        }
    }
}