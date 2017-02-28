using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EKE.Data.Repository
{
    public interface IEntityBaseRepository<T> where T : class, new()
    {
        void Add(T entity);
        void Update(T entity);

        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);

        T GetById(int id);
        T GetByIdIncluding(int id, params Expression<Func<T, object>>[] includeProperties);
        T Get(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetAllIncludingPred(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindByIncluding(Expression<Func<T, bool>> predicate, Func<T, object> orderBy, bool ascending, int pageIndex, int pageSize, params Expression<Func<T, object>>[] includeProperties);

        int Count();
        int Count(Expression<Func<T, bool>> predicate);
    }
}
