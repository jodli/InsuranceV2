namespace InsuranceV2.Infrastructure.Database.Context
{
    public static class DataContextFactory
    {
        public static void Clear()
        {
            var dataContextStorageContainer = DataContextStorageFactory<InsuranceAppContext>.CreateStorageContainer();
            dataContextStorageContainer.Clear();
        }

        public static InsuranceAppContext GetDataContext()
        {
            var dataContextStorageContainer = DataContextStorageFactory<InsuranceAppContext>.CreateStorageContainer();
            var insuranceAppContext = dataContextStorageContainer.GetDataContext();

            if (insuranceAppContext == null)
            {
                insuranceAppContext = new InsuranceAppContext();
                dataContextStorageContainer.Store(insuranceAppContext);
            }
            return insuranceAppContext;
        }
    }
}