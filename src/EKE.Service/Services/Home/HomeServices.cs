using System;
using EKE.Data.Infrastructure;

namespace EKE.Service.Services.Home
{
    public interface IHomeServices
    {
    }

    public class HomeServices : IHomeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
