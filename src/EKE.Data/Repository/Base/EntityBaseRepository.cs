using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EKE.Data.Entities.Base;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace EKE.Data.Repository.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        #region Properties
        private readonly BaseDbContext _dataContext;
        private readonly DbSet<T> _dbSet;
        #endregion

        public EntityBaseRepository(BaseDbContext dbContext)
        {
            _dataContext = dbContext;
            _dbSet = _dataContext.Set<T>();
        }

        #region IEntityBaseRepository Implementation
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
            //dataContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(predicate).AsEnumerable();
            foreach (T obj in objects)
            {
                _dbSet.Remove(obj);
            }
        }

        public virtual T GetById(int id)
        {
            return _dbSet.FirstOrDefault(p => p.Id == id);
        }

        public virtual T GetByIdIncluding(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault(o => o.Id == id);
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual IEnumerable<T> FindByIncluding(Expression<Func<T, bool>> predicate, Func<T, object> orderBy, bool ascending, int pageIndex, int pageSize, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            query = query.AsExpandable().Where(predicate);
            query = ascending ? query.OrderBy(orderBy).AsQueryable() : query.OrderByDescending(orderBy).AsQueryable();
            return PaginateSource(query, pageIndex, pageSize).ToList();
        }

        public virtual int Count()
        {
            return _dbSet.Count();
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsExpandable().Where(predicate).Count();
        }
        #endregion

        protected static IQueryable<T> PaginateSource(IQueryable<T> source, int pageIndex, int pageSize)
        {
            return (source.Skip((pageIndex - 1) * pageSize).Take(pageSize));
        }

        public IEnumerable<T> GetAllIncludingPred(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsExpandable().Where(predicate).ToList();
        }
    }
}
