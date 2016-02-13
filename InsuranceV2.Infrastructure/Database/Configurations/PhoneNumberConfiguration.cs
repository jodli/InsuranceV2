using System.Data.Entity.ModelConfiguration;
using InsuranceV2.Common.Models;

namespace InsuranceV2.Infrastructure.Database.Configurations
{
    public class PhoneNumberConfiguration : EntityTypeConfiguration<PhoneNumber>
    {
        public PhoneNumberConfiguration()
        {
            Property(x => x.Number).HasMaxLength(25);
        }
    }
}