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

        List<MediaElement> CreateMediaElements(IFormFile files, int year, string section);

        Result<List<Order>> GetAllOrders();
        Result AddOrder(Order model);
        Result DeleteOrder(int id);

        Result<List<Synonym>> GetAllSynonyms();
        Result<Synonym> AddSynonym(string text);
        Result DeleteSynonym(int id);
        Result ConnectSynonym(int id, string text);
        Result UpdateSynonym(XEditSM model);

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
        private readonly IEntityBaseRepository<Synonym> _synonymRepo;

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
            IEntityBaseRepository<Synonym> synonymRepository,
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
            _synonymRepo = synonymRepository;
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
                    model.MediaElements = CreateMediaElements(model.Files, model.PublishYear, model.PublishSection);
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

        #region MediaElements
        public List<MediaElement> CreateMediaElements(IFormFile files, int year, string section)
        {
#warning MEGOLDANI AZ EGESZ METODUST ATNEZNI!
            var gyoparPath = _environment.WebRootPath.Replace("EKE-Admin.Web", "EKE-Gyopar.Web");
            var relativePath = String.Format("Uploads/{0}/{1}", year.ToString().Trim(), section.Trim());
            var uploads = Path.Combine(gyoparPath, relativePath);
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var mediaElements = new List<MediaElement>();
            if (files.Length > 0)
            {
                var uploadPath = Path.Combine(uploads, files.FileName);
                using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                {
                    files.CopyTo(fileStream);
                }
                var fileName = String.Format("r1_{0}", files.FileName);
                var outputPath = Path.Combine(uploads, fileName);
                var resize = ResizeImage(uploadPath, outputPath, MediaTypesScope.Cover);

                var mediaElem = new MediaElement();
                mediaElem.OriginalName = String.Format("{0}/{1}", relativePath, resize.IsOk() ? fileName : files.FileName);
                mediaElem.Description = string.Format("{0}_{1}", year.ToString().Trim(), section.Trim());
                mediaElem.Name = _generalService.RandomString(10);
                mediaElem.Type = MediaTypesEnum.Image;
                mediaElem.Scope = MediaTypesScope.Cover;
                mediaElements.Add(mediaElem);
            }

            return mediaElements;
        }

        public Result ResizeImage(string inputPath, string outputPath, MediaTypesScope type)
        {
            var size = 0;
            var quality = 0;

            switch (type)
            {
                case MediaTypesScope.Background:
                    break;
                case MediaTypesScope.Cover:
                    size = 400;
                    quality = 75;
                    break;
                case MediaTypesScope.Article:
                    break;
                default:
                    break;
            }

            try
            {
                using (var image = new MagickImage(inputPath))
                {
                    image.Resize(size, size);
                    image.Strip();
                    image.Quality = quality;
                    image.Write(outputPath);
                    return new Result(ResultStatus.OK);
                }
            }
            catch (Exception)
            {
                return new Result(ResultStatus.EXCEPTION);
            }
        }
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

            //var removable = result.MediaElements.FirstOrDefault(x => x.Scope == Data.Entities.Enums.MediaTypesScope.Cover);
            //result.MediaElements.Remove(removable);

            if (files == null || files.Length == 0) return new Result<Magazine>(ResultStatus.ERROR);

            result.MediaElements = CreateMediaElements(files, result.PublishYear, result.PublishSection);

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

        #region Synonyms
        public Result<List<Synonym>> GetAllSynonyms()
        {
            var result = _synonymRepo.GetAllIncludingPred(x => x.Main, x => x.Synonyms).ToList();
            return new Result<List<Synonym>>(result);
        }

        public Result<Synonym> AddSynonym(string text)
        {
            var exists = _synonymRepo.FindBy(x => x.Name == text);
            if (exists.Count() > 0) return new Result<Synonym>(ResultStatus.ALREADYEXISTS);

            Synonym model = new Synonym();
            model.Name = text;
            model.Main = true;

            _synonymRepo.Add(model);
            SaveChanges();
            return new Result<Synonym>(model);
        }

        public Result DeleteSynonym(int id)
        {
            var result = _synonymRepo.GetByIdIncluding(id, x => x.Synonyms);
            if (result == null) return new Result(ResultStatus.NOT_FOUND);

            foreach (var item in result.Synonyms)
            {
                _synonymRepo.Delete(result);
                SaveChanges();
            }

            _synonymRepo.Delete(result);
            SaveChanges();
            return new Result(ResultStatus.OK);
        }

        public Result ConnectSynonym(int id, string text)
        {
            var result = _synonymRepo.GetByIdIncluding(id, x => x.Synonyms);
            if (result == null) return new Result(ResultStatus.NOT_FOUND);

            var newSynonym = new Synonym();
            newSynonym.Name = text;
            newSynonym.Main = false;

            result.Synonyms.Add(newSynonym);
            _synonymRepo.Update(result);
            SaveChanges();
            return new Result(ResultStatus.OK);
        }

        public Result UpdateSynonym(XEditSM model)
        {
            var result = _synonymRepo.GetByIdIncluding(model.PrimaryKey, x => x.Synonyms);
            if (result == null) return new Result(ResultStatus.NOT_FOUND);

            if (String.IsNullOrEmpty(model.Value))
            {
                _synonymRepo.Delete(result);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }

            result.Name = model.Value;
            _synonymRepo.Update(result);
            SaveChanges();
            return new Result(ResultStatus.OK);
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
