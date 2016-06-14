using InsuranceV2.Common.Enums;

namespace InsuranceV2.Application.Models.EmailAddress
{
    public class DetailEmailAddress
    {
        public int Id { get; set; }
        public string EmailAddressText { get; set; }
        public ContactType ContactType { get; set; }
    }
}