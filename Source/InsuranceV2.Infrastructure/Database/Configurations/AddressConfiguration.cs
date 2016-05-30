using System.Data.Entity.ModelConfiguration;
using InsuranceV2.Common.Models;

namespace InsuranceV2.Infrastructure.Database.Configurations
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            Property(x => x.Street).HasMaxLength(250);
            Property(x => x.StreetNumber).HasMaxLength(25);
            Property(x => x.ZipCode).HasMaxLength(25);
            Property(x => x.City).HasMaxLength(250);
            Property(x => x.Country).HasMaxLength(250);
        }
    }
}