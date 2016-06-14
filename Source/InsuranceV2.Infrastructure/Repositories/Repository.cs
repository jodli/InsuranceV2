using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using InsuranceV2.Common;
using InsuranceV2.Infrastructure.Database.Context;

namespace InsuranceV2.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T, int>, IDisposable where T : DomainEntity<int>
    {
        public void Dispose()
        {
            if (DataContextFactory.GetDataContext() != null)
            {
                DataContextFactory.GetDataContext().Dispose();
            }
        }

        public T FindById(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties).SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = DataContextFactory.GetDataContext().Set<T>();

            if (includeProperties != null)
            {
                items = includeProperties.Aggregate(items,
                    (current, includeProperty) => current.Include(includeProperty));
            }

            return items;
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = DataContextFactory.GetDataContext().Set<T>();

            if (includeProperties != null)
            {
                items = includeProperties.Aggregate(items,
                    (current, includeProperty) => current.Include(includeProperty));
            }

            return items.Where(predicate);
        }

        public void Add(T entity)
        {
            DataContextFactory.GetDataContext().Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            DataContextFactory.GetDataContext().Set<T>().Remove(entity);
        }

        public void Remove(int id)
        {
            Remove(FindById(id));
        }
    }
}