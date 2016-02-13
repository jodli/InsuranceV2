using System;
using System.Collections;
using System.Threading;

namespace InsuranceV2.Infrastructure.Database.Context
{
    public class ThreadDataContextStorageContainer<T> : IDataContextStorageContainer<T> where T : class
    {
        private static readonly Hashtable StoredContexts = new Hashtable();

        public T GetDataContext()
        {
            T context = null;

            var threadName = GetThreadName();
            if (StoredContexts.Contains(threadName))
            {
                context = (T) StoredContexts[threadName];
            }

            return context;
        }

        public void Store(T objectContext)
        {
            var threadName = GetThreadName();
            if (StoredContexts.Contains(threadName))
            {
                StoredContexts[threadName] = objectContext;
            }
            else
            {
                StoredContexts.Add(threadName, objectContext);
            }
        }

        public void Clear()
        {
            var threadName = GetThreadName();
            if (StoredContexts.Contains(threadName))
            {
                StoredContexts[threadName] = null;
            }
        }

        private static string GetThreadName()
        {
            if (string.IsNullOrEmpty(Thread.CurrentThread.Name))
            {
                Thread.CurrentThread.Name = Guid.NewGuid().ToString();
            }
            return Thread.CurrentThread.Name;
        }
    }
}