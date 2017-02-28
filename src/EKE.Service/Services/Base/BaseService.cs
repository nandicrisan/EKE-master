using EKE.Data.Infrastructure;

namespace EKE.Service.Services
{
    public abstract class BaseService : IBaseService
    {
        #region Properties
        protected readonly IUnitOfWork unitOfWork;
        #endregion

        public BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region IBaseService Members
        public virtual void SaveChanges()
        {
            unitOfWork.Commit();
        }
        #endregion
    }
}
