using System;
using System.Linq;
using FluentAssertions;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;
using NUnit.Framework;

namespace InsuranceV2.Tests.Unit
{
    [TestFixture]
    public class InsuranceTests
    {
        public static Insurance CreateInsurance()
        {
            return new Insurance
            {
                InsuranceNumber = "1234567890"
            };
        }

        [Test]
        public void CanCreateInstanceOfInsurance()
        {
            var insurance = new Insurance();
            insurance.Should().NotBeNull();
            insurance.Id.Should().Be(0);
        }

        [Test]
        public void CanSetOwnerToInsurance()
        {
            var insurance = new Insurance();
            var insuree = new Insuree();

            insurance.Owner = insuree;

            insurance.Owner.ShouldBeEquivalentTo(insuree);
        }

        [Test]
        public void CanSetInsuranceNumberToInsurance()
        {
            var insurance = new Insurance();

            insurance.InsuranceNumber = "12345";

            insurance.InsuranceNumber.Should().Be("12345");
        }

        [Test]
        public void CanSetInsurerToInsurance()
        {
            var insurance = new Insurance();

            insurance.Company = InsuranceCompany.Vhv;

            insurance.Company.ShouldBeEquivalentTo(InsuranceCompany.Vhv);
        }

        [Test]
        public void CanSetTypeOfInsurance()
        {
            var insurance = new Insurance();

            insurance.Type = InsuranceType.Php;

            insurance.Type.ShouldBeEquivalentTo(InsuranceType.Php);
        }

        [Test]
        public void CanSetStartDateOfInsurance()
        {
            var insurance = new Insurance();

            insurance.StartDate = DateTime.Today;

            insurance.StartDate.ShouldBeEquivalentTo(DateTime.Today);
        }

        [Test]
        public void CanSetContractDateOfInsurance()
        {
            var insurance = new Insurance();

            insurance.ContractDate = DateTime.Today;

            insurance.ContractDate.ShouldBeEquivalentTo(DateTime.Today);
        }

        [Test]
        public void CanSetCancelledToInsurance()
        {
            var insurance = new Insurance();

            insurance.Cancelled = true;

            insurance.Cancelled.ShouldBeEquivalentTo(true);
        }

        [Test]
        public void CanSetEmployeeToInsurance()
        {
            var insurance = new Insurance();

            insurance.Employee = "";

            insurance.Employee.Should().NotBeNull();
        }

        [Test]
        public void CanSetLicensePlateToInsurance()
        {
            var insurance = new Insurance();

            insurance.LicensePlate = "AB-ABC 123";

            insurance.LicensePlate.ShouldBeEquivalentTo("AB-ABC 123");
        }

        [Test]
        public void NewInsuranceIsCancelled()
        {
            var insurance = new Insurance();

            insurance.Cancelled.ShouldBeEquivalentTo(false);
        }

        [Test]
        public void DetectNullInsuranceNumber()
        {
            var insurance = new Insurance();

            insurance.InsuranceNumber = null;

            var errors = insurance.Validate();
            errors.Should().Contain(x => x.MemberNames.Contains("InsuranceNumber"));
        }

        [Test]
        public void DetectEmptyInsuranceNumber()
        {
            var insurance = new Insurance();

            insurance.InsuranceNumber = "";

            var errors = insurance.Validate();
            errors.Should().Contain(x => x.MemberNames.Contains("InsuranceNumber"));
        }

        [Test]
        public void DetectStartDateInThePast()
        {
            var insurance = new Insurance();

            insurance.StartDate = DateTime.Now.AddDays(-1);

            var errors = insurance.Validate();
            errors.Should().Contain(x => x.MemberNames.Contains("StartDate"));
        }
    }
}