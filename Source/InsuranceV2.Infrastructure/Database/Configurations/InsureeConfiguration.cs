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
        }
    }
}