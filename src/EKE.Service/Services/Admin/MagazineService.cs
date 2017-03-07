using EKE.Data.Entities.Gyopar;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EKE.Service.Services.Admin
{
    public interface IMagazineService : IBaseService
    {
        Result<List<Magazine>> GetAllMagazines();
        Result<Magazine> GetMagazineById(int id);
        Result<Magazine> Add(Magazine model);
        Result<Magazine> Update(Magazine model);

        Result<List<Article>> GetAllArticles();
        Result<Article> GetArticleById(int id);
        Result<Article> Add(Article model);
        Result<Article> Update(Article model);

        Result<MagazineCategory> Add(MagazineCategory model);
        Result<List<MagazineCategory>> GetAllMagazineCategories();

        Result<bool> DeleteMagazineCategory(int id);
        Result<bool> DeleteMagazine(int id);
    }

    public class MagazineService : BaseService, IMagazineService
    {
        private readonly IEntityBaseRepository<Magazine> _magazineRepo;
        private readonly IEntityBaseRepository<Article> _articleRepo;
        private readonly IEntityBaseRepository<MagazineCategory> _magazineCatRepo;

        public MagazineService(IEntityBaseRepository<Magazine> magazineRepository, IEntityBaseRepository<Article> articleRepository, IEntityBaseRepository<MagazineCategory> magazineCatRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _magazineRepo = magazineRepository;
            _articleRepo = articleRepository;
            _magazineCatRepo = magazineCatRepository;
        }

        #region Magazines
        #region CRUD
        public Result<List<Magazine>> GetAllMagazines()
        {
            return new Result<List<Magazine>>(_magazineRepo.GetAll().ToList());
        }

        public Result<Magazine> GetMagazineById(int id)
        {
            return new Result<Magazine>(_magazineRepo.GetById(id));
        }

        public Result<Magazine> Add(Magazine model)
        {
            try
            {
                var exists = _magazineRepo.FindBy(x => x.PublishYear == model.PublishYear && x.PublishSection == model.PublishSection);
                if (exists.Count() > 0)
                    return new Result<Magazine>(ResultStatus.ALREADYEXISTS, "A lapszám már létezik! Kérem ellenőrizze az adatokat!");

                var category = _magazineCatRepo.GetById(model.Category.Id);
                if (category == null)
                    return new Result<Magazine>(ResultStatus.ERROR, "Hiba a kategória lekérése során");

                model.Category = category;
                model.DateCreated = DateTime.Now;
                model.Slug = GenerateSlug(model.Title, model.PublishYear, model.PublishSection);
                _magazineRepo.Add(model);
                SaveChanges();
                return new Result<Magazine>(model);
            }
            catch (Exception ex)
            {
                return new Result<Magazine>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result<Magazine> Update(Magazine model)
        {
            _magazineRepo.Update(model);
            SaveChanges();
            return new Result<Magazine>(model);
        }

        public Result<bool> DeleteMagazine(int id)
        {
            try
            {
                var magazine = _magazineRepo.GetById(id);
                _magazineRepo.Delete(magazine);
                SaveChanges();
                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                return new Result<bool>(ResultStatus.ERROR, ex.Message);
            }
        }
        #endregion
        #endregion

        #region Articles
        #region CRUD
        public Result<List<Article>> GetAllArticles()
        {
            try
            {
                return new Result<List<Article>>(_articleRepo.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new Result<List<Article>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<Article> GetArticleById(int id)
        {
            try
            {
                return new Result<Article>(_articleRepo.GetById(id));
            }
            catch (Exception ex)
            {
                return new Result<Article>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<Article> Add(Article model)
        {
            try
            {
                _articleRepo.Add(model);
                SaveChanges();
                return new Result<Article>(model);
            }
            catch (Exception ex)
            {
                return new Result<Article>(ResultStatus.ERROR, ex.Message);
            }

        }

        public Result<Article> Update(Article model)
        {
            try
            {
                _articleRepo.Update(model);
                SaveChanges();
                return new Result<Article>(model);
            }
            catch (Exception ex)
            {
                return new Result<Article>(ResultStatus.ERROR, ex.Message);
            }

        }
        #endregion
        #endregion

        #region MagazineCategories
        public Result<List<MagazineCategory>> GetAllMagazineCategories()
        {

            try
            {
                return new Result<List<MagazineCategory>>(_magazineCatRepo.GetAll().ToList());
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
                var magazine = _magazineCatRepo.GetById(id);
                _magazineCatRepo.Delete(magazine);
                SaveChanges();
                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                return new Result<bool>(ResultStatus.ERROR, ex.Message);
            }
        }
        #endregion

        #region General
        public string GenerateSlug(string phrase, int year, int section)
        {
            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            str = string.Format("{0}_{1}_{2}", str, year, section);
            return str;
        }

        public string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
        #endregion
    }
}
