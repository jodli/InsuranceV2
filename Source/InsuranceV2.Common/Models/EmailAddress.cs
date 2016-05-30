using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Enums;

namespace InsuranceV2.Common.Models
{
    public class EmailAddress : DomainEntity<int>, IHasOwner<Insuree>
    {
        [EmailAddress]
        public string EmailAddressText { get; set; }

        public ContactType ContactType { get; set; }

        public int OwnerId { get; set; }
        public Insuree Owner { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ContactType == ContactType.None)
            {
                yield return new ValidationResult("ContactType can't be None.", new[] {"ContactType"});
            }
        }
    }
}