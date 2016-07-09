using System.Data.Entity.ModelConfiguration;
using InsuranceV2.Common.Models;

namespace InsuranceV2.Infrastructure.Database.Configurations
{
    public class InsuranceConfiguration : EntityTypeConfiguration<Insurance>
    {
        public InsuranceConfiguration()
        {
            Property(x => x.InsuranceNumber).IsRequired().HasMaxLength(50);

            Property(x => x.StartDate).HasColumnType("date");
            Property(x => x.ContractDate).HasColumnType("date");
        }
    }
}