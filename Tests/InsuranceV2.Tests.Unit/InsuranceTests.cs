using System;
using System.Management.Instrumentation;
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

            insurance.Insurer = InsuranceCompany.VHV;

            insurance.Insurer.ShouldBeEquivalentTo(InsuranceCompany.VHV);
        }

        [Test]
        public void CanSetTypeOfInsurance()
        {
            var insurance = new Insurance();

            insurance.Type = InsuranceType.PHP;

            insurance.Type.ShouldBeEquivalentTo(InsuranceType.PHP);
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
    }
}