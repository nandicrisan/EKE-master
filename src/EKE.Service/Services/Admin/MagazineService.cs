using EKE.Data.Entities.Gyopar;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }

    public class MagazineService : BaseService, IMagazineService
    {
        private readonly IEntityBaseRepository<Magazine> _magazineRepo;
        private readonly IEntityBaseRepository<Article> _articleRepo;

        public MagazineService(IEntityBaseRepository<Magazine> magazineRepository, IEntityBaseRepository<Article> articleRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _magazineRepo = magazineRepository;
            _articleRepo = articleRepository;
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
            _magazineRepo.Add(model);
            SaveChanges();
            return new Result<Magazine>(model);
        }

        public Result<Magazine> Update(Magazine model)
        {
            _magazineRepo.Update(model);
            SaveChanges();
            return new Result<Magazine>(model);
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
            _articleRepo.Add(model);
            SaveChanges();
            return new Result<Article>(model);
        }

        public Result<Article> Update(Article model)
        {
            _articleRepo.Update(model);
            SaveChanges();
            return new Result<Article>(model);
        }
        #endregion
        #endregion

    }
}
