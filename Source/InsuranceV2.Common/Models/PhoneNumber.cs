using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Enums;

namespace InsuranceV2.Common.Models
{
    public class PhoneNumber : DomainEntity<int>, IHasOwner<Insuree>
    {
        public string Number { get; set; }
        public ContactType ContactType { get; set; }
        public PhoneType PhoneType { get; set; }
        public int OwnerId { get; set; }
        public Insuree Owner { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Number))
            {
                yield return new ValidationResult("Number can't be null or empty.", new[] {"Number"});
            }
            if (ContactType == ContactType.None)
            {
                yield return new ValidationResult("ContactType can't be None.", new[] {"ContactType"});
            }
            if (PhoneType == PhoneType.None)
            {
                yield return new ValidationResult("PhoneType can't be None.", new[] {"PhoneType"});
            }
        }
    }
}