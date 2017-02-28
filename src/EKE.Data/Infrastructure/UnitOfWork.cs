using System;

namespace EKE.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private BaseDbContext dbContext;

        public UnitOfWork(BaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public BaseDbContext DbContext
        {
            get { return dbContext; }
        }

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
