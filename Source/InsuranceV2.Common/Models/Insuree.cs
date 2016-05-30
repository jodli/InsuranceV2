using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Collections;

namespace InsuranceV2.Common.Models
{
    public class Insuree : DomainEntity<int>, IDateTracking
    {
        public Insuree()
        {
            Addresses = new Addresses();
            EmailAddresses = new EmailAddresses();
            PhoneNumbers = new PhoneNumbers();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Addresses Addresses { get; set; }
        public EmailAddresses EmailAddresses { get; }
        public PhoneNumbers PhoneNumbers { get; set; }

        //Todo: married to
        //Todo: married since
        //Todo: divorced since

        public string FullName
        {
            get
            {
                var ret = LastName ?? string.Empty;
                if (!string.IsNullOrEmpty(FirstName))
                {
                    if (ret.Length > 0)
                    {
                        ret += ", ";
                    }
                    ret += FirstName;
                }
                return ret;
            }
        }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfBirth < DateTime.Now.AddYears(-Constants.MaxAgeInsuree) || DateOfBirth > DateTime.Now)
            {
                yield return
                    new ValidationResult(
                        $"Invalid range for date of birth; must be between {0} and {Constants.MaxAgeInsuree} years.",
                        new[] {"DateOfBirth"});
            }
            foreach (var validationResult in Addresses.Validate())
            {
                yield return validationResult;
            }
            foreach (var validationResult in EmailAddresses.Validate())
            {
                yield return validationResult;
            }
            foreach (var validationResult in PhoneNumbers.Validate())
            {
                yield return validationResult;
            }
        }
    }
}