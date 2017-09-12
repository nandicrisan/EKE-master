using EKE.Data.DataViewModels;
using EKE.Data.Entities.Gyopar;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.Utils;
using LinqKit;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EKE.Service.Services.Admin
{
    public interface IArticleService : IBaseService
    {
        Result<List<Article>> Get(ArticleSearch model);
        Result<List<Article>> GetSelected();
        Result<int> Count(ArticleSearch filter);

        Result<List<Article>> GetAllArticles();
        Result<List<Article>> GetAllArticlesBy(Expression<Func<Article, bool>> predicate);
        Result<Article> GetArticleBySlug(string slug);
        Result<Article> GetArticleById(int id);
        Result<Article> Add(Article model, string userName);
        Result<Article> Update(Article model, string username);
        Result DeleteArticle(int id);

        Result FormatHtml();
    }

    public class ArticleService : BaseService, IArticleService
    {
        private readonly IEntityBaseRepository<Article> _articleRepo;
        private readonly IEntityBaseRepository<MagazineCategory> _magazineCatRepo;
        private readonly IEntityBaseRepository<Magazine> _magazineRepo;
        private readonly IEntityBaseRepository<Author> _authorRepo;
        private readonly IEntityBaseRepository<MediaElement> _mediaElementRepo;
        private readonly IEntityBaseRepository<Synonym> _synonymRepo;

        private readonly IGeneralService _generalService;
        private readonly IHostingEnvironment _environment;

        public ArticleService(
            IEntityBaseRepository<Magazine> magazineRepository,
            IEntityBaseRepository<Article> articleRepo,
            IEntityBaseRepository<MagazineCategory> magazineCatRepository,
            IEntityBaseRepository<Author> authorRepository,
            IEntityBaseRepository<MediaElement> mediaElementRepository,
            IEntityBaseRepository<Synonym> synonymRepository,

            IHostingEnvironment environment,
            IGeneralService generalService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _magazineRepo = magazineRepository;
            _articleRepo = articleRepo;
            _magazineCatRepo = magazineCatRepository;
            _authorRepo = authorRepository;
            _mediaElementRepo = mediaElementRepository;
            _synonymRepo = synonymRepository;

            _environment = environment;
            _generalService = generalService;
        }

        #region ArticleSearch
        public Result<List<Article>> Get(ArticleSearch filter)
        {
            try
            {
                var predicate = GetSearchPredicate(filter);
                var result = _articleRepo.FindByIncluding(predicate, GetSortedFunction(filter), filter.OrderDirection, filter.Page, filter.Display, p => p.Magazine, p => p.Author);
                return new Result<List<Article>>(result.Distinct().ToList());
            }
            catch (Exception ex)
            {
                return new Result<List<Article>>(ex);
            }
        }

        public Result<int> Count(ArticleSearch filter)
        {
            try
            {
                var predicate = GetSearchPredicate(filter);
                var result = _articleRepo.Count(predicate);
                return new Result<int>(result);
            }
            catch (Exception ex)
            {
                return new Result<int>(ex);
            }
        }

        private static Func<Article, object> GetSortedFunction(ArticleSearch filter)
        {
            if (filter == null) return new Func<Article, object>(q => q.DateCreated);
            switch (filter.Order)
            {
                case 1:
                    return new Func<Article, object>(c => c.Title);
                default:
                    return new Func<Article, object>(q => q.DateCreated);
            }
        }

        private Expression<Func<Article, bool>> GetSearchPredicate(ArticleSearch filter)
        {
            var predicate = PredicateBuilder.New<Article>(true);
            if (filter.Keyword != null)
            {
                //predicate = predicate.And(p => p.ArticleTag.Any(q => q.Tag.Name.Contains(filter.Keyword)) || p.Author.Name.ToLower().Contains(filter.Keyword) ||
                // p.Content.ToLower().Contains(filter.Keyword) || p.Title.ToLower().Contains(filter.Keyword));
                var dictionary = GetAllByName(filter.Keyword);
                predicate = predicate.And(p => p.Content.ToLower().Contains(filter.Keyword) || p.Title.ToLower().Contains(filter.Keyword));

                if (dictionary.Count > 0)
                {
                    foreach (var item in dictionary)
                    {
                        predicate = predicate.Or(p => p.Content.ToLower().Contains(item.Name) || p.Title.ToLower().Contains(item.Name));
                    }
                }
            }

            if (filter.Author != null)
            {
                predicate = predicate.And(p => p.Author.Name.ToLower().Contains(filter.Author));
            }

            predicate = predicate.And(p => p.Magazine != null);
            if (filter.RangeTypeYear)
            {
                predicate = predicate.And(p => p.Magazine.PublishYear >= Convert.ToInt32(filter.PublishYearRange.FirstOrDefault()));
            }
            else
            {
                if (filter.PublishYearRange.Count > 0)
                {
                    foreach (var item in filter.PublishYearRange)
                    {
                        predicate = predicate.And(p => p.Magazine.PublishYear == Convert.ToInt32(filter.PublishYearRange.FirstOrDefault()));
                    }
                }
            }

            if (filter.RangeTypeSection)
            {
                predicate = predicate.And(p => p.Magazine.PublishSection.Contains(filter.PublishSectionRange.FirstOrDefault()));
            }
            else
            {
                if (filter.PublishSectionRange.Count > 0)
                {
                    foreach (var item in filter.PublishSectionRange)
                    {
                        predicate = predicate.And(p => p.Magazine.PublishSection.Contains(filter.PublishSectionRange.FirstOrDefault()));
                    }
                }
            }
            return predicate;
        }

        public Result<List<Article>> GetSelected()
        {
            try
            {
                var result = _articleRepo.GetAllIncludingPred(x => x.Selected, p => p.Magazine, p => p.Author).OrderByDescending(x => x.DateCreated).Take(6).ToList();
                return new Result<List<Article>>(result);
            }
            catch (Exception ex)
            {
                return new Result<List<Article>>(ResultStatus.EXCEPTION, ex.Message);
            }
        }
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
                        mediaElem.Name = _generalService.RandomString(10);
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
                    model.Magazine.Slug = _generalService.GenerateSlug(model.Magazine.Title, model.Magazine.PublishYear, model.Magazine.PublishSection);
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
                model.Slug = _generalService.GenerateSlug(model.Title, model.Magazine.PublishYear, model.Magazine.PublishSection);
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
                        mediaElem.Name = _generalService.RandomString(10);
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
                    model.Magazine.Slug = _generalService.GenerateSlug(model.Magazine.Title, model.Magazine.PublishYear, model.Magazine.PublishSection);
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

        #region Other
        public Result FormatHtml()
        {
            var articles = _articleRepo.GetAll();
            foreach (var item in articles)
            {
                if (!string.IsNullOrEmpty(item.Content))
                {
                    var content = item.Content;

                    content = content.Replace("\r\n", "<br />");
                    item.Content = content;

                    _articleRepo.Update(item);
                    SaveChanges();
                }
            }
            return new Result(ResultStatus.OK);
        }

        public List<Synonym> GetAllByName(string name)
        {
            var result = _synonymRepo.GetAllIncludingPred(x => x.Name == name.Trim(), x => x.Synonyms).FirstOrDefault();
            if (result == null) return new List<Synonym>();
            if (result.Synonyms.Count == 0)
            {
                result = _synonymRepo.GetAllIncluding(x => x.Synonyms).FirstOrDefault(e => e.Synonyms
                                 .Any(a => a.Name == name));
                if (result == null) return new List<Synonym>();
                result.Synonyms = result.Synonyms.Where(x => x.Name != name).ToList();
                var list = result.Synonyms;
                result.Synonyms = null;
                list.Add(result);
                return list.ToList();
            }

            return result.Synonyms.ToList();
        }
        #endregion
    }
}
