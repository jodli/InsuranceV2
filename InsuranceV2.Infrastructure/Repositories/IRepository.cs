using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InsuranceV2.Infrastructure.Repositories
{
    public interface IRepository<T, in TK> where T : class
    {
        T FindById(TK id, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);

        void Add(T entity);
        void Remove(T entity);
        void Remove(TK id);
    }
}