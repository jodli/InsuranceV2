using System.Web;

namespace InsuranceV2.Infrastructure.Database.Context
{
    public static class DataContextStorageFactory<T> where T : class
    {
        private static IDataContextStorageContainer<T> _dataContextStorageContainer;

        public static IDataContextStorageContainer<T> CreateStorageContainer()
        {
            if (_dataContextStorageContainer == null)
            {
                if (HttpContext.Current != null)
                {
                    _dataContextStorageContainer = new HttpDataContextStorageContainer<T>();
                }
                else
                {
                    _dataContextStorageContainer = new ThreadDataContextStorageContainer<T>();
                }
            }
            return _dataContextStorageContainer;
        }
    }
}