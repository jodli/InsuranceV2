using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Enums;

namespace InsuranceV2.Common.Models
{
    public class Address : DomainEntity<int>, IHasOwner<Insuree>
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ContactType ContactType { get; set; }

        public int OwnerId { get; set; }
        public Insuree Owner { get; set; }

        public bool IsNull => string.IsNullOrEmpty(Street) &&
                              string.IsNullOrEmpty(StreetNumber) &&
                              string.IsNullOrEmpty(ZipCode) &&
                              string.IsNullOrEmpty(City) &&
                              string.IsNullOrEmpty(Country);

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsNull) yield break;

            if (ContactType == ContactType.None)
            {
                yield return new ValidationResult($"ContactType can't be None", new[] {"ContactType"});
            }
            if (string.IsNullOrEmpty(Street))
            {
                yield return new ValidationResult($"Street can't be null or empty", new[] {"Street"});
            }
            if (string.IsNullOrEmpty(StreetNumber))
            {
                yield return new ValidationResult($"StreetNumber can't be null or empty", new[] {"StreetNumber"});
            }
            if (string.IsNullOrEmpty(ZipCode))
            {
                yield return new ValidationResult($"ZipCode can't be null or empty", new[] {"ZipCode"});
            }
            if (string.IsNullOrEmpty(City))
            {
                yield return new ValidationResult($"City can't be null or empty", new[] {"City"});
            }
            if (string.IsNullOrEmpty(Country))
            {
                yield return new ValidationResult($"Country can't be null or empty", new[] {"Country"});
            }
        }
    }
}