using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Collections;
using InsuranceV2.Common.Enums;

namespace InsuranceV2.Common.Models
{
    public class Insuree : DomainEntity<int>, IDateTracking
    {
        public Insuree()
        {
            HomeAddress = new Address(null, null, null, null, null, ContactType.Personal);
            WorkAddress = new Address(null, null, null, null, null, ContactType.Business);
            EmailAddresses = new EmailAddresses();
            PhoneNumbers = new PhoneNumbers();
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Address HomeAddress { get; set; }
        public Address WorkAddress { get; set; }

        public EmailAddresses EmailAddresses { get; }
        public PhoneNumbers PhoneNumbers { get; set; }

        public string FullName
        {
            get
            {
                var ret = FirstName ?? string.Empty;
                if (!string.IsNullOrEmpty(LastName))
                {
                    if (ret.Length > 0)
                    {
                        ret += " ";
                    }
                    ret += LastName;
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
            foreach (var validationResult in HomeAddress.Validate())
            {
                yield return validationResult;
            }
            foreach (var validationResult in WorkAddress.Validate())
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