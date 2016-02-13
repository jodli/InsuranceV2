using InsuranceV2.Common.Enums;

namespace InsuranceV2.Application.Models.Address
{
    public class DetailAddress
    {
        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public ContactType ContactType { get; set; }
    }
}