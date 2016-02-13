using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;

namespace InsuranceV2.Common.Collections
{
    public class EmailAddresses : CollectionBase<EmailAddress>
    {
        public EmailAddresses()
        {
        }

        public EmailAddresses(IList<EmailAddress> initialList) : base(initialList)
        {
        }

        public EmailAddresses(CollectionBase<EmailAddress> initialList) : base(initialList)
        {
        }

        public void Add(string emailAddressText, ContactType contactType)
        {
            Add(new EmailAddress
            {
                EmailAddressText = emailAddressText,
                ContactType = contactType
            });
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var emailAddress in this)
            {
                errors.AddRange(emailAddress.Validate());
            }
            return errors;
        }
    }
}