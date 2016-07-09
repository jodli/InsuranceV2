using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;

namespace InsuranceV2.Common.Collections
{
    public class Insurances : CollectionBase<Insurance>
    {
        public Insurances()
        {
        }

        public Insurances(IList<Insurance> initialList) : base(initialList)
        {
        }

        public Insurances(CollectionBase<Insurance> initialList) : base(initialList)
        {
        }

        public void Add(string insuranceNumber, DateTime startDate, DateTime contractDate, bool cancelled,
            InsuranceCompany company, string employee, string licensePlate, InsuranceType type)
        {
            Add(new Insurance
            {
                InsuranceNumber = insuranceNumber,
                StartDate = startDate,
                ContractDate = contractDate,
                Cancelled = cancelled,
                Company = company,
                Employee = employee,
                LicensePlate = licensePlate,
                Type = type
            });
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var insurance in this)
            {
                errors.AddRange(insurance.Validate());
            }
            return errors;
        }
    }
}