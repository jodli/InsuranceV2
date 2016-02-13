using System.Data.Entity.ModelConfiguration;
using InsuranceV2.Common.Models;

namespace InsuranceV2.Infrastructure.Database.Configurations
{
    public class InsureeConfiguration : EntityTypeConfiguration<Insuree>
    {
        public InsureeConfiguration()
        {
            Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            Property(x => x.LastName).IsRequired().HasMaxLength(50);

            Property(x => x.HomeAddress.Street).HasColumnName("HomeAddressStreet").HasMaxLength(50);
            Property(x => x.HomeAddress.StreetNumber).HasColumnName("HomeAddressStreetNumber").HasMaxLength(10);
            Property(x => x.HomeAddress.ZipCode).HasColumnName("HomeAddressZipCode").HasMaxLength(10);
            Property(x => x.HomeAddress.City).HasColumnName("HomeAddressCity").HasMaxLength(50);
            Property(x => x.HomeAddress.Country).HasColumnName("HomeAddressCountry").HasMaxLength(50);
            Property(x => x.HomeAddress.ContactType).HasColumnName("HomeAddressContactType");

            Property(x => x.WorkAddress.Street).HasColumnName("WorkAddressStreet").HasMaxLength(50);
            Property(x => x.WorkAddress.StreetNumber).HasColumnName("WorkAddressStreetNumber").HasMaxLength(10);
            Property(x => x.WorkAddress.ZipCode).HasColumnName("WorkAddressZipCode").HasMaxLength(10);
            Property(x => x.WorkAddress.City).HasColumnName("WorkAddressCity").HasMaxLength(50);
            Property(x => x.WorkAddress.Country).HasColumnName("WorkAddressCountry").HasMaxLength(50);
            Property(x => x.WorkAddress.ContactType).HasColumnName("WorkAddressContactType");
        }
    }
}