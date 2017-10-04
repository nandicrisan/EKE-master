using EKE.Data.Entities.Gyopar;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.ServiceModel;
using EKE.Service.Utils;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using ImageMagick;
using EKE.Data.Entities.Enums;

namespace EKE.Service.Services.Admin
{
    public interface IMagazineService : IBaseService
    {
        Result<List<Magazine>> GetAllMagazines();
        Result<List<Magazine>> GetAllMagazinesIncluding();
        Result<List<Magazine>> GetLastMagazines(int count);
        Result<List<Magazine>> GetAllMagazinesBy(Expression<Func<Magazine, bool>> predicate);

        Result<Magazine> GetMagazineById(int id);
        Result<Magazine> Add(Magazine model);
        Result<Magazine> Update(Magazine model);
        Result<Magazine> Update(XEditSM model);
        Result<Magazine> UpdateCover(IFormFile files, int id);
        Result<Magazine> GetMagazinesBy(Expression<Func<Magazine, bool>> predicate);

        Result<bool> DeleteMagazine(int id);

        Result<List<Order>> GetAllOrders();
        Result AddOrder(Order model);
        Result DeleteOrder(int id);

        Result<List<Tag>> GetAllTags();
        Result<Tag> Add(Tag model);
        Result DeleteTag(int id);

        Result<List<Author>> GetAllAuthors();
    }

    public class MagazineService : BaseService, IMagazineService
    {
        private readonly IEntityBaseRepository<Magazine> _magazineRepo;
        private readonly IEntityBaseRepository<MagazineCategory> _magazineCatRepo;
        private readonly IEntityBaseRepository<Tag> _tagRepo;
        private readonly IEntityBaseRepository<MediaElement> _mediaElementRepo;
        private readonly IEntityBaseRepository<Author> _authorRepo;
        private readonly IEntityBaseRepository<Order> _orderRepo;

        private readonly IHostingEnvironment _environment;
        private readonly IGeneralService _generalService;

        public MagazineService(
            IEntityBaseRepository<Magazine> magazineRepository,
            IEntityBaseRepository<Article> articleRepository,
            IEntityBaseRepository<MagazineCategory> magazineCatRepository,
            IEntityBaseRepository<Tag> tagRepository,
            IEntityBaseRepository<MediaElement> mediaElementRepository,
            IEntityBaseRepository<Author> authorRepository,
            IEntityBaseRepository<Order> orderRepository,
            IHostingEnvironment environment,
            IGeneralService generalService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _magazineRepo = magazineRepository;
            _magazineCatRepo = magazineCatRepository;
            _tagRepo = tagRepository;
            _mediaElementRepo = mediaElementRepository;
            _authorRepo = authorRepository;
            _orderRepo = orderRepository;
            _environment = environment;
            _generalService = generalService;
        }

        #region Magazines
        #region CRUD
        public Result<List<Magazine>> GetAllMagazines()
        {
            return new Result<List<Magazine>>(_magazineRepo.GetAll().ToList());
        }

        public Result<List<Magazine>> GetAllMagazinesIncluding()
        {
            return new Result<List<Magazine>>(_magazineRepo.GetAllIncluding(x => x.Articles, x => x.Category, x => x.MediaElements).ToList());
        }

        public Result<List<Magazine>> GetLastMagazines(int count)
        {
            try
            {
                var result = _magazineRepo.GetAllIncludingPred(x => x.Visible, x => x.Articles, x => x.Category, x => x.MediaElements).OrderByDescending(x => x.DateCreated).Take(count).ToList();
                CheckMediaElements(result);
                return new Result<List<Magazine>>(result);
            }
            catch (Exception ex)
            {
                return new Result<List<Magazine>>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result<List<Magazine>> GetAllMagazinesBy(Expression<Func<Magazine, bool>> predicate)
        {
            try
            {
                var result = _magazineRepo.GetAllIncludingPred(predicate, x => x.Author, x => x.Articles, x => x.MediaElements).ToList();
                CheckMediaElements(result);
                return new Result<List<Magazine>>(result);
            }
            catch (Exception ex)
            {
                return new Result<List<Magazine>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<Magazine> GetMagazineById(int id)
        {
            try
            {
                var result = _magazineRepo.GetByIdIncluding(id, x => x.Articles, x => x.MediaElements);
                CheckMediaElements(result);
                return new Result<Magazine>(result);
            }
            catch (Exception ex)
            {
                return new Result<Magazine>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<Magazine> Add(Magazine model)
        {
            try
            {

                var category = _magazineCatRepo.GetById(model.Category.Id);
                if (category == null)
                    return new Result<Magazine>(ResultStatus.ERROR, "Hiba a kategória lekérése során");

                model.Category = category;

                var exists = _magazineRepo.FindBy(x => x.PublishYear == model.PublishYear && x.PublishSection.Contains(model.PublishSection) && x.Category.Id == model.Category.Id);
                if (exists.Any())
                    return new Result<Magazine>(ResultStatus.ALREADYEXISTS, "A lapszám már létezik! Kérem ellenőrizze az adatokat!");

                if (model.Files != null)
                {
                    ICollection<IFormFile> files = new List<IFormFile>();
                    files.Add(model.Files);
                    model.MediaElements = _generalService.CreateMediaElements(files, model.PublishYear, model.PublishSection, ProjectBaseEnum.Gyopar);
                }

                model.DateCreated = DateTime.Now;
                model.Slug = _generalService.GenerateSlug(model.Title, model.PublishYear, model.PublishSection);
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
                var magazine = _magazineRepo.GetByIdIncluding(id, x => x.MediaElements, x => x.Articles);
                _magazineRepo.Delete(magazine);
                SaveChanges();
                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                return new Result<bool>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<Magazine> GetMagazinesBy(Expression<Func<Magazine, bool>> predicate)
        {
            try
            {
                var result = _magazineRepo.GetAllIncludingPred(predicate, x => x.Author, x => x.Articles).FirstOrDefault();
                return new Result<Magazine>(result);
            }
            catch (Exception ex)
            {
                return new Result<Magazine>(ResultStatus.ERROR, ex.Message);
            }
        }
        #endregion
        #endregion

        #region Tags
        public Result<List<Tag>> GetAllTags()
        {
            try
            {
                var result = _tagRepo.GetAll();
                if (result == null)
                    return new Result<List<Tag>>(ResultStatus.NOT_FOUND);
                return new Result<List<Tag>>(result.ToList());
            }
            catch (Exception ex)
            {
                return new Result<List<Tag>>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result<Tag> Add(Tag model)
        {
            try
            {
                model.Name = model.Name.Trim();
                var exists = _tagRepo.FindBy(x => x.Name == model.Name.Trim());
                if (exists.Count() > 0)
                    return new Result<Tag>(ResultStatus.ALREADYEXISTS, "A megadott kulcsszó már létezik!");

                _tagRepo.Add(model);
                SaveChanges();
                return new Result<Tag>(model);
            }
            catch (Exception ex)
            {
                return new Result<Tag>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result DeleteTag(int id)
        {
            try
            {
                var result = _tagRepo.GetById(id);
                if (result == null)
                    return new Result(ResultStatus.NOT_FOUND);

                _tagRepo.Delete(result);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }
        #endregion

        #region Author
        public Result<List<Author>> GetAllAuthors()
        {
            try
            {
                var result = _authorRepo.GetAll();
                if (result == null)
                    return new Result<List<Author>>(ResultStatus.NOT_FOUND);
                return new Result<List<Author>>(result.ToList());

            }
            catch (Exception e)
            {
                return new Result<List<Author>>(ResultStatus.EXCEPTION, e.Message);
            }
        }
        #endregion

        #region XEdit
        public Result<Magazine> Update(XEditSM model)
        {
            var result = _magazineRepo.GetById(model.PrimaryKey);
            if (result == null) return new Result<Magazine>(ResultStatus.NOT_FOUND);

            switch (model.Name)
            {
                case "Visible":
                    var visible = Convert.ToBoolean(model.Value);
                    if (result.Visible == visible) return new Result<Magazine>(result);
                    result.Visible = visible;
                    break;
                case "Title":
                    if (result.Title.Trim() == model.Value.Trim()) return new Result<Magazine>(result);
                    result.Title = model.Value.Trim();
                    break;
                case "Section":
                    if (result.PublishSection.Trim() == model.Value.Trim()) return new Result<Magazine>(result);
                    result.PublishSection = model.Value.Trim();
                    break;
                case "Year":
                    var year = Convert.ToInt32(model.Value);
                    if (result.PublishYear == year) return new Result<Magazine>(result);
                    result.PublishYear = year;
                    break;
                default:
                    return new Result<Magazine>(result);
            }

            _magazineRepo.Update(result);
            SaveChanges();
            return new Result<Magazine>(result);
        }

        public Result<Magazine> UpdateCover(IFormFile files, int id)
        {
            var result = _magazineRepo.GetByIdIncluding(id, x => x.MediaElements);

            if (result == null) return new Result<Magazine>(ResultStatus.NOT_FOUND);

            if (files == null || files.Length == 0) return new Result<Magazine>(ResultStatus.ERROR);

            ICollection<IFormFile> filesCollection = new List<IFormFile>();
            filesCollection.Add(files);
            result.MediaElements = _generalService.CreateMediaElements(filesCollection, result.PublishYear, result.PublishSection, ProjectBaseEnum.Gyopar);

            _magazineRepo.Update(result);
            SaveChanges();
            return new Result<Magazine>(result);
        }
        #endregion

        #region Orders
        public Result AddOrder(Order model)
        {
            var result = _orderRepo.FindBy(x => x.Email == model.Email && x.PhoneNumber == model.PhoneNumber && x.Name == model.Name);
            if (result.Count() > 0) return new Result(ResultStatus.ALREADYEXISTS);

            _orderRepo.Add(model);
            SaveChanges();
            return new Result(ResultStatus.OK);
        }

        public Result<List<Order>> GetAllOrders()
        {
            var result = _orderRepo.GetAll().ToList();
            return new Result<List<Order>>(result);
        }

        public Result DeleteOrder(int id)
        {
            var result = _orderRepo.GetById(id);
            if (result == null) return new Result(ResultStatus.NOT_FOUND);

            try
            {
                _orderRepo.Delete(result);
                SaveChanges();

                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }

        }
        #endregion



        #region Private
        private void CheckMediaElements(List<Magazine> magazines)
        {
            if (magazines.Count > 0)
            {
                foreach (var magazine in magazines)
                {
                    if (magazine.MediaElements.Count == 0)
                    {
                        var mediaElement = new MediaElement()
                        {
                            Description = "Borito",
                            Name = "Template borito",
                            OriginalName = "images/components/EGY_borito_H.jpg",
                        };
                        magazine.MediaElements.Add(mediaElement);
                    }
                }
            }
        }

        private void CheckMediaElements(Magazine magazine)
        {
            if (magazine != null)
            {
                if (magazine.MediaElements.Count == 0)
                {
                    var mediaElement = new MediaElement()
                    {
                        Description = "Borito",
                        Name = "Template borito",
                        OriginalName = "images/components/template_borito.jpg",
                    };
                    magazine.MediaElements.Add(mediaElement);
                }
            }
        }
        #endregion
    }
}
