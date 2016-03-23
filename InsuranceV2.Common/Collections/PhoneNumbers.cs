using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;

namespace InsuranceV2.Common.Collections
{
    public class PhoneNumbers : CollectionBase<PhoneNumber>
    {
        public PhoneNumbers()
        {
        }

        public PhoneNumbers(IList<PhoneNumber> initialList) : base(initialList)
        {
        }

        public PhoneNumbers(CollectionBase<PhoneNumber> initialList) : base(initialList)
        {
        }

        public void Add(string number, PhoneType phoneType, ContactType contactType)
        {
            Add(new PhoneNumber
            {
                Number = number,
                PhoneType = phoneType,
                ContactType = contactType
            });
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var phoneNumber in this)
            {
                errors.AddRange(phoneNumber.Validate());
            }
            return errors;
        }
    }
}