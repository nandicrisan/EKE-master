using EKE.Data.Entities;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.Utils;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EKE.Service.Services
{
    public interface IGeneralService : IBaseService
    {
        //Result<List<WorkShop>> GetWorkShpos();
    }

    // public class GeneralService : BaseService, IGeneralService
    //{
    //private readonly IEntityBaseRepository<WorkShop> workShopRepository;
    //public GeneralService(IEntityBaseRepository<WorkShop> workShopRepository, IEntityBaseRepository<RegisterStatus> registerStatusRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
    //{
    //    this.workShopRepository = workShopRepository;
    //    this.registerStatusRepository = registerStatusRepository;
    //}

    //public Result<RegisterStatus> GetRegisterStatus()
    //{
    //    var registerStatus = registerStatusRepository.GetAll().ToList();
    //    if (registerStatus.Count() > 0)
    //        return new Result<RegisterStatus>(registerStatus.FirstOrDefault());

    //    return new Result<RegisterStatus>(ResultStatus.EMPTY);
    //}

    //  }
}
