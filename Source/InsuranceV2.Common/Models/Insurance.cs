using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Enums;

namespace InsuranceV2.Common.Models
{
    public class Insurance : DomainEntity<int>, IHasOwner<Insuree>
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(InsuranceNumber))
            {
                yield return new ValidationResult("InsuranceNumber can't be null or empty.", new[] {"InsuranceNumber"});
            }
            if (StartDate < DateTime.Today)
            {
                yield return new ValidationResult("StartDate can't be in the past.", new[] {"StartDate"});
            }
            if (Type == InsuranceType.Kfz && string.IsNullOrWhiteSpace(LicensePlate))
            {
                yield return new ValidationResult("LicensePlate can't be null or empty.", new[] {"LicensePlate"});
            }
        }

        public bool Cancelled { get; set; }

        public DateTime ContractDate { get; set; }
        public DateTime StartDate { get; set; }

        public InsuranceType Type { get; set; }
        public InsuranceCompany Company { get; set; }

        public string LicensePlate { get; set; }
        public string InsuranceNumber { get; set; }

        public int OwnerId { get; set; }
        public Insuree Owner { get; set; }

        public string Employee { get; set; }
    }
}