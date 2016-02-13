using InsuranceV2.Infrastructure.Database.Context;

namespace InsuranceV2.Infrastructure.Database.Initializer
{
    public static class InsuranceAppContextInitializer
    {
        public static void Init(bool dropDatabaseIfModelChanges)
        {
            if (dropDatabaseIfModelChanges)
            {
                System.Data.Entity.Database.SetInitializer(new MyDropCreateDatabaseIfModelChanges());
                using (var db = new InsuranceAppContext())
                {
                    db.Database.Initialize(false);
                }
            }
            else
            {
                System.Data.Entity.Database.SetInitializer<InsuranceAppContext>(null);
            }
        }
    }
}