using System;
using EKE.Data.Infrastructure;

namespace EKE.Service.Services.Home
{
    public interface IHomeServices : IBaseService
    {

    }
    public class HomeServices : BaseService, IHomeServices
    {
        public HomeServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
