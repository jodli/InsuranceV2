using InsuranceV2.Common.Enums;

namespace InsuranceV2.Application.Models.PhoneNumber
{
    public class DetailPhoneNumber
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public ContactType ContactType { get; set; }

        public PhoneType PhoneType { get; set; }

        public override string ToString()
        {
            return $"{ContactType.ToString("G")} {PhoneType.ToString("G")}: {Number}";
        }
    }
}