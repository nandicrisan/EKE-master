using EKE.Data.Entities.Gyopar;
using EKE.Data.Infrastructure;
using EKE.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EKE.Service.Services.Admin
{
    public interface IMagazineCategoryService
    {
        Result<MagazineCategory> Add(MagazineCategory model);
        Result<List<MagazineCategory>> GetAllMagazineCategories();
        Result<MagazineCategory> GetMagazineCategoryById(int id);
        Result<bool> DeleteMagazineCategory(int id);
    }

    public class MagazineCategoryService : IMagazineCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MagazineCategoryService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region MagazineCategories
        public Result<List<MagazineCategory>> GetAllMagazineCategories()
        {
            try
            {
                var magazineCategories = _unitOfWork.MagazineCategoryRepository.GetAllIncluding(x => x.Magazines).ToList();
                return magazineCategories.Count == 0 ? new Result<List<MagazineCategory>>(ResultStatus.NOT_FOUND) : new Result<List<MagazineCategory>>(magazineCategories);
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
                var exists = _unitOfWork.MagazineCategoryRepository.FindBy(x => x.Name == model.Name);
                if (exists.Any())
                    return new Result<MagazineCategory>(ResultStatus.ALREADYEXISTS, "A folyóirat már létezik! Kérem ellenőrizze az adatokat!");

                _unitOfWork.MagazineCategoryRepository.Add(model);
                _unitOfWork.SaveChanges();
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
                var magazine = _unitOfWork.MagazineCategoryRepository.GetByIdIncluding(id, x => x.Magazines);

                if (magazine == null)
                    return new Result<bool>(ResultStatus.NOT_FOUND, "Folyóirat nem található!");

                if (magazine.Magazines != null && magazine.Magazines.Count > 0)
                {
                    foreach (var item in magazine.Magazines)
                    {
                        _unitOfWork.MagazineRepository.Delete(item);
                    }
                }

                _unitOfWork.MagazineCategoryRepository.Delete(magazine);
                _unitOfWork.SaveChanges();
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
                return new Result<MagazineCategory>(_unitOfWork.MagazineCategoryRepository.GetById(id));
            }
            catch (Exception ex)
            {
                return new Result<MagazineCategory>(ResultStatus.ERROR, ex.Message);
            }
        }
        #endregion
    }
}
