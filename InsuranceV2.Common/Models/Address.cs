using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Enums;

namespace InsuranceV2.Common.Models
{
    public class Address : ValueObject<Address>
    {
        private Address()
        {
        }

        public Address(string street, string streetNumber, string zipCode, string city, string country,
            ContactType contactType)
        {
            Street = street;
            StreetNumber = streetNumber;
            ZipCode = zipCode;
            City = city;
            Country = country;
            ContactType = contactType;
        }

        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        public ContactType ContactType { get; private set; }

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