namespace InsuranceV2.Infrastructure.Database.Context
{
    public interface IDataContextStorageContainer<T>
    {
        T GetDataContext();

        void Store(T objectContext);

        void Clear();
    }
}