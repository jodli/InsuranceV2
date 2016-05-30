using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;

namespace InsuranceV2.Common.Collections
{
    public class Addresses : CollectionBase<Address>
    {
        public Addresses()
        {
        }

        public Addresses(IList<Address> initialList) : base(initialList)
        {
        }

        public Addresses(CollectionBase<Address> initialList) : base(initialList)
        {
        }

        public void Add(string street, string streetNumber, string zipCode, string city, string country,
            ContactType contactType)
        {
            Add(new Address
            {
                Street = street,
                StreetNumber = streetNumber,
                ZipCode = zipCode,
                City = city,
                Country = country,
                ContactType = contactType
            });
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var address in this)
            {
                errors.AddRange(address.Validate());
            }
            return errors;
        }
    }
}