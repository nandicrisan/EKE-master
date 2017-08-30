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
        Result<Magazine> UpdateVisibility(XEditSM model);
        Result<Magazine> GetMagazinesBy(Expression<Func<Magazine, bool>> predicate);

        Result<List<Article>> GetAllArticles();
        Result<List<Article>> GetAllArticlesBy(Expression<Func<Article, bool>> predicate);
        Result<Article> GetArticleBySlug(string slug);
        Result<Article> GetArticleById(int id);
        Result<Article> Add(Article model, string userName);
        Result<Article> Update(Article model, string username);
        Result DeleteArticle(int id);

        Result<MagazineCategory> Add(MagazineCategory model);
        Result<List<MagazineCategory>> GetAllMagazineCategories();
        Result<MagazineCategory> GetMagazineCategoryById(int id);

        Result<bool> DeleteMagazineCategory(int id);
        Result<bool> DeleteMagazine(int id);

        Result<List<Tag>> GetAllTags();
        Result<Tag> Add(Tag model);
        Result DeleteTag(int id);

        Result<List<Author>> GetAllAuthors();
    }

    public class MagazineService : BaseService, IMagazineService
    {
        private readonly IEntityBaseRepository<Magazine> _magazineRepo;
        private readonly IEntityBaseRepository<Article> _articleRepo;
        private readonly IEntityBaseRepository<MagazineCategory> _magazineCatRepo;
        private readonly IEntityBaseRepository<Tag> _tagRepo;
        private readonly IEntityBaseRepository<MediaElement> _mediaElementRepo;
        private readonly IEntityBaseRepository<Author> _authorRepo;
        private readonly IHostingEnvironment _environment;

        public MagazineService(
            IEntityBaseRepository<Magazine> magazineRepository,
            IEntityBaseRepository<Article> articleRepository,
            IEntityBaseRepository<MagazineCategory> magazineCatRepository,
            IEntityBaseRepository<Tag> tagRepository,
            IEntityBaseRepository<MediaElement> mediaElementRepository,
            IEntityBaseRepository<Author> authorRepository,
            IHostingEnvironment environment,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _magazineRepo = magazineRepository;
            _articleRepo = articleRepository;
            _magazineCatRepo = magazineCatRepository;
            _tagRepo = tagRepository;
            _mediaElementRepo = mediaElementRepository;
            _authorRepo = authorRepository;
            _environment = environment;
        }

        #region Magazines
        #region CRUD
        public Result<List<Magazine>> GetAllMagazines()
        {
            return new Result<List<Magazine>>(_magazineRepo.GetAll().ToList());
        }

        public Result<List<Magazine>> GetAllMagazinesIncluding()
        {
            return new Result<List<Magazine>>(_magazineRepo.GetAllIncluding(x => x.Articles, x => x.Category).ToList());
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

                //if (model.Files != null)
                //{
                //    var uploads = Path.Combine(_environment.WebRootPath, String.Format("Uploads/{0}/{1}", model.PublishYear, model.PublishSection));
                //    if (!Directory.Exists(uploads))
                //        Directory.CreateDirectory(uploads);

                //    var mediaElements = new List<MediaElement>();
                //    if (model.Files.Length > 0)
                //    {
                //        using (var fileStream = new FileStream(Path.Combine(uploads, model.Files.FileName), FileMode.Create))
                //        {
                //            model.Files.CopyToAsync(fileStream);
                //        }
                //    }
                //    var mediaElem = new MediaElement();
                //    mediaElem.OriginalName = String.Format("{0}/{1}", uploads, model.Files.Name);
                //    mediaElem.Name = RandomString(10);
                //    mediaElem.Type = Data.Entities.Enums.MediaTypesEnum.Pdf;
                //    mediaElements.Add(mediaElem);

                //    model.MediaElements = mediaElements;
                //}

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

        public Result<List<Article>> GetAllArticlesBy(Expression<Func<Article, bool>> predicate)
        {
            try
            {
                var result = _articleRepo.GetAllIncludingPred(predicate, x => x.Author, x => x.Magazine).ToList();
                return new Result<List<Article>>(result);
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
                var result = _articleRepo.GetByIdIncluding(id, x => x.Author, x => x.MediaElement, x => x.ArticleTag, x => x.Magazine, x => x.Magazine.Category);
                if (result == null) return new Result<Article>(ResultStatus.NOT_FOUND);
                return new Result<Article>(result);
            }
            catch (Exception ex)
            {
                return new Result<Article>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result<Article> GetArticleBySlug(string slug)
        {
            try
            {
                var result = _articleRepo.GetAllIncludingPred(x => x.Slug == slug, x => x.Author, x => x.MediaElement, x => x.ArticleTag, x => x.Magazine, x => x.Magazine.Category).FirstOrDefault();
                if (result == null) return new Result<Article>(ResultStatus.NOT_FOUND);
                return new Result<Article>(result);
            }
            catch (Exception ex)
            {
                return new Result<Article>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result<Article> Add(Article model, string userName)
        {
            try
            {
                if (model.Files != null)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, String.Format("Uploads/{0}/{1}", model.Magazine.PublishYear, model.Magazine.PublishSection));
                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    var mediaElements = new List<MediaElement>();
                    foreach (var file in model.Files)
                    {
                        if (file.Length > 0)
                        {
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                file.CopyToAsync(fileStream);
                            }
                        }
                        var mediaElem = new MediaElement();
                        mediaElem.OriginalName = String.Format("{0}/{1}", uploads, file.Name);
                        mediaElem.Name = RandomString(10);
                        mediaElem.Type = Data.Entities.Enums.MediaTypesEnum.Image;
                        mediaElements.Add(mediaElem);
                    }

                    model.MediaElement = mediaElements;
                }

                var magCat = _magazineCatRepo.GetById(model.Magazine.Category.Id);
                if (magCat == null)
                    return new Result<Article>(ResultStatus.NOT_FOUND, "Folyóirat nem található");

                var magazine = _magazineRepo.FindBy(x => x.PublishYear == model.Magazine.PublishYear && x.PublishSection.Contains(model.Magazine.PublishSection) && x.Category.Id == model.Magazine.Category.Id);
                if (!magazine.Any())
                {
                    model.Magazine.Category = magCat;
                    model.Magazine.Title = String.Format("{0} / {1}", model.Magazine.PublishYear, model.Magazine.PublishSection);
                    model.Magazine.Slug = GenerateSlug(model.Magazine.Title, model.Magazine.PublishYear, model.Magazine.PublishSection);
                    model.Magazine.DateCreated = DateTime.Now;
                }
                else
                {
                    model.Magazine = magazine.FirstOrDefault();
                }

                var author = new Author();
                if (model.Author.Id == 0)
                {
                    author = new Author { Name = model.Author.Name };
                }
                else
                {
                    author = _authorRepo.GetById(model.Author.Id);
                }
                model.Author = author;
                model.Slug = GenerateSlug(model.Title, model.Magazine.PublishYear, model.Magazine.PublishSection);
                model.PublishedBy = userName;
                model.DateCreated = DateTime.Now;

                //foreach (var item in model.ArticleTags)
                //{
                //    var tag = _tagRepo.GetById(Convert.ToInt32(item));
                //}

                _articleRepo.Add(model);
                SaveChanges();
                return new Result<Article>(model);
            }
            catch (Exception ex)
            {
                return new Result<Article>(ResultStatus.ERROR, ex.Message);
            }

        }

        public Result<Article> Update(Article model, string username)
        {
            try
            {
                if (model.Files != null)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, String.Format("Uploads/{0}/{1}", model.Magazine.PublishYear, model.Magazine.PublishSection));
                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    var mediaElements = new List<MediaElement>();
                    foreach (var file in model.Files)
                    {
                        if (file.Length > 0)
                        {
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                file.CopyToAsync(fileStream);
                            }
                        }
                        var mediaElem = new MediaElement();
                        mediaElem.OriginalName = String.Format("{0}/{1}", uploads, file.Name);
                        mediaElem.Name = RandomString(10);
                        mediaElem.Type = Data.Entities.Enums.MediaTypesEnum.Image;
                        mediaElements.Add(mediaElem);
                    }

                    model.MediaElement = mediaElements;
                }

                var magCat = _magazineCatRepo.GetById(model.Magazine.Category.Id);
                if (magCat == null)
                    return new Result<Article>(ResultStatus.NOT_FOUND, "Folyóirat nem található");

                var magazine = _magazineRepo.FindBy(x => x.PublishYear == model.Magazine.PublishYear && x.PublishSection.Contains(model.Magazine.PublishSection) && x.Category.Id == model.Magazine.Category.Id);
                if (!magazine.Any())
                {
                    model.Magazine.Category = magCat;
                    model.Magazine.Title = String.Format("{0} / {1}", model.Magazine.PublishYear, model.Magazine.PublishSection);
                    model.Magazine.Slug = GenerateSlug(model.Magazine.Title, model.Magazine.PublishYear, model.Magazine.PublishSection);
                    model.Magazine.DateCreated = DateTime.Now;
                }
                else
                {
                    model.Magazine = magazine.FirstOrDefault();
                }

                var author = new Author();
                if (model.Author.Id == 0)
                {
                    author = new Author { Name = model.Author.Name };
                }
                else
                {
                    author = _authorRepo.GetById(model.Author.Id);
                }
                model.Author = author;
                model.PublishedBy = username;

                //foreach (var item in model.ArticleTags)
                //{
                //    var tag = _tagRepo.GetById(Convert.ToInt32(item));
                //}

                _articleRepo.Update(model);
                SaveChanges();
                return new Result<Article>(model);
            }
            catch (Exception ex)
            {
                return new Result<Article>(ResultStatus.ERROR, ex.Message);
            }

        }

        public Result DeleteArticle(int id)
        {
            try
            {
                var article = _articleRepo.GetByIdIncluding(id, x => x.MediaElement);
                foreach (var item in article.MediaElement)
                {
                    _mediaElementRepo.Delete(item);
                }
                _articleRepo.Delete(article);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }
        #endregion
        #endregion

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

        #region General
        public string GenerateSlug(string phrase, int year, string section)
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


        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
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

        #region XEdit
        public Result<Magazine> UpdateVisibility(XEditSM model)
        {
            var visible = Convert.ToBoolean(model.Value);
            var result = _magazineRepo.GetById(model.PrimaryKey);

            if (result == null) return new Result<Magazine>(ResultStatus.NOT_FOUND);
            if (result.Visible == visible) return new Result<Magazine>(result);

            result.Visible = visible;
            _magazineRepo.Update(result);
            SaveChanges();
            return new Result<Magazine>(result);
        }
        #endregion
    }
}
