using EKE.Data.Entities.Gyopar;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EKE.Service.Services.Admin
{
    public interface IMagazineCategoryService : IBaseService
    {
        Result<MagazineCategory> Add(MagazineCategory model);
        Result<List<MagazineCategory>> GetAllMagazineCategories();
        Result<MagazineCategory> GetMagazineCategoryById(int id);
        Result<bool> DeleteMagazineCategory(int id);
    }

    public class MagazineCategoryService : BaseService, IMagazineCategoryService
    {
        private readonly IEntityBaseRepository<Magazine> _magazineRepo;
        private readonly IEntityBaseRepository<MagazineCategory> _magazineCatRepo;

        public MagazineCategoryService(
            IEntityBaseRepository<Magazine> magazineRepository,
            IEntityBaseRepository<MagazineCategory> magazineCatRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _magazineRepo = magazineRepository;
            _magazineCatRepo = magazineCatRepository;
        }

        #region MagazineCategories
        public Result<List<MagazineCategory>> GetAllMagazineCategories()
        {
            try
            {
                var magazineCategories = _magazineCatRepo.GetAllIncluding(x => x.Magazines).ToList();
                if (magazineCategories.Count == 0)
                    return new Result<List<MagazineCategory>>(ResultStatus.NOT_FOUND);

                return new Result<List<MagazineCategory>>(magazineCategories);
            }
            catch (Exception ex)
            {
                return new Result<List<MagazineCategory>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<MagazineCategory> Add(MagazineCategory model)
        {
            try
            {
                var exists = _magazineCatRepo.FindBy(x => x.Name == model.Name);
                if (exists.Count() > 0)
                    return new Result<MagazineCategory>(ResultStatus.ALREADYEXISTS, "A folyóirat már létezik! Kérem ellenőrizze az adatokat!");

                _magazineCatRepo.Add(model);
                SaveChanges();
                return new Result<MagazineCategory>(model);
            }
            catch (Exception ex)
            {
                return new Result<MagazineCategory>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<bool> DeleteMagazineCategory(int id)
        {
            try
            {
                var magazine = _magazineCatRepo.GetByIdIncluding(id, x => x.Magazines);

                if (magazine == null)
                    return new Result<bool>(ResultStatus.NOT_FOUND, "Folyóirat nem található!");

                if (magazine.Magazines != null && magazine.Magazines.Count > 0)
                {
                    foreach (var item in magazine.Magazines)
                    {
                        _magazineRepo.Delete(item);
                    }
                }

                _magazineCatRepo.Delete(magazine);
                SaveChanges();
                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                return new Result<bool>(ResultStatus.ERROR, ex.Message);
            }
        }


        public Result<MagazineCategory> GetMagazineCategoryById(int id)
        {
            try
            {
                return new Result<MagazineCategory>(_magazineCatRepo.GetById(id));
            }
            catch (Exception ex)
            {
                return new Result<MagazineCategory>(ResultStatus.ERROR, ex.Message);
            }
        }
        #endregion
    }
}
