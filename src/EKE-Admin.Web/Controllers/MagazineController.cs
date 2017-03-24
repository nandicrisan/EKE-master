using AutoMapper;
using EKE.Data.Entities.Gyopar;
using EKE.Service.Services.Admin;
using EKE_Admin.Web.ViewModels;
using EKE_Admin.Web.ViewModels.Configuration;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EKE_Admin.Web.Controllers
{
    [Authorize(Roles = "superadmin,gyopar")]
    [AutoValidateAntiforgeryToken]
    public class MagazineController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMagazineService _magService;
        public MagazineController(IMagazineService magazineService, IMapper mapperService)
        {
            _magService = magazineService;
            _mapper = mapperService;
        }

        public IActionResult Index()
        {
            var magazineCategories = _magService.GetAllMagazineCategories();
            if (!magazineCategories.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", magazineCategories.Status, magazineCategories.StatusText);
                return View(new MagazineVM());
            }

            var tags = _magService.GetAllTags();
            if (!tags.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", tags.Status, tags.Message);
                return View(new MagazineVM());
            }

            var mapper = _mapper.Map<MagazineVM>(magazineCategories.Data).Map(tags.Data);
            return View(mapper);
        }

        #region Magazine
        public IActionResult MagazineList()
        {
            var magazineCategories = _magService.GetAllMagazineCategories();
            if (!magazineCategories.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", magazineCategories.Status, magazineCategories.Message);
                return View(new MagazineListVM());
            }

            var tags = _magService.GetAllTags();
            if (!tags.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", tags.Status, tags.Message);
                return View(new MagazineListVM());
            }

            MagazineListVM viewmodel = _mapper.Map<MagazineListVM>(magazineCategories.Data).Map(tags.Data);
            return View(viewmodel);
        }

        public IActionResult MagazineListGrid()
        {
            var magazines = _magService.GetAllMagazinesIncluding();
            if (!magazines.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", magazines.Status, magazines.Message);
                return PartialView("Partials/_MagazineListGrid", new List<Magazine>());
            }

            // Only grid string query values will be visible here.
            return PartialView("Partials/_MagazineListGrid", magazines.Data);
        }

        [HttpPost]
        public IActionResult AddMagazine(Magazine model)
        {
            ModelState.Remove("Category.Name");
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = string.Format("Hiba a validáció során! Kérem töltsön ki minden mezőt!");
                return RedirectToAction("MagazineList");
            }
            var magazines = _magService.Add(model);
            if (magazines.IsOk())
                return RedirectToAction("MagazineList");

            TempData["ErrorMessage"] = string.Format("Hiba a hozzáadás során ({0} : {1})", magazines.Status, magazines.Message);
            return RedirectToAction("MagazineList");
        }

        [HttpPost]
        public IActionResult AddMagazineCategory(MagazineCategory model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = string.Format("Hiba a validáció során! Kérem töltsön ki minden szükséges mezőt!");
                return RedirectToAction("MagazineList");
            }
            var magazines = _magService.Add(model);
            if (magazines.IsOk())
                return RedirectToAction("MagazineList");

            TempData["ErrorMessage"] = string.Format("Hiba a hozzáadás során ({0} : {1})", magazines.Status, magazines.Message);
            return RedirectToAction("MagazineList");
        }

        public IActionResult DeleteMagazineCategory(int id)
        {
            var magazines = _magService.DeleteMagazineCategory(id);
            if (magazines.IsOk())
                return RedirectToAction("MagazineList");

            TempData["ErrorMessage"] = string.Format("Hiba a törlés során ({0} : {1})", magazines.Status, magazines.Message);
            return RedirectToAction("MagazineList");
        }

        public IActionResult DeleteMagazine(int id)
        {
            var magazines = _magService.DeleteMagazine(id);
            if (magazines.IsOk())
                return RedirectToAction("MagazineList");

            TempData["ErrorMessage"] = string.Format("Hiba a törlés során ({0} : {1})", magazines.Status, magazines.Message);
            return RedirectToAction("MagazineList");
        }

        #endregion

        #region Article
        public IActionResult ArticleGrid(int format = 0, int year = 0, int section = 0)
        {
            //get articles via Magazine->Category route
            var predicate = PredicateBuilder.New<Article>();
            if (format != 0)
                predicate.And(x => x.Magazine.Category.Id == format);

            if (year != 0)
                predicate.And(x => x.Magazine.PublishYear == year);

            if (section != 0)
                predicate.And(x => x.Magazine.PublishSection.Contains(String.Format("{0}", section)));

            var result = _magService.GetAllArticlesBy(predicate);
            if (!result.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", result.Status, result.Message);
                return PartialView("Partials/_ArticleGrid");
            }

            // Only grid string query values will be visible here.
            return PartialView("Partials/_ArticleGrid", result.Data);
        }

        public IActionResult CreateArticlePartial(int format = 0, int year = 0, int section = 0)
        {
            //TODO: solve error handling
            var magazineCategory = new MagazineCategory();
            magazineCategory.Id = format;

            var magazine = new Magazine();
            magazine.Category = magazineCategory;
            magazine.PublishYear = year;
            magazine.PublishSection = String.Format("{0}", section);

            var model = new Article();
            model.Magazine = magazine;

            var tags = _magService.GetAllTags();
            if (!tags.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", tags.Status, tags.Message);
                return PartialView("Partials/_AddArticle");
            }

            var articleVM = new ArticleVM();
            articleVM.Article = model;
            articleVM.Tags = tags.Data;
            // Only grid string query values will be visible here.
            return PartialView("Partials/_AddArticle", articleVM);
        }

        [HttpPost]
        public IActionResult AddArticle(ArticleVM model)
        {
            //TODO: attach tags to the article
            var message = "Sikeresen hozzáadva!";
            ModelState.Remove("Article.Slug");
            ModelState.Remove("Article.Magazine.Category.Name");
            ModelState.Remove("Article.Magazine.Title");
            if (!ModelState.IsValid)
            {
                message = "Hiba a validáció során. A mezők kitöltése kötelező!";
                return PartialView("Layout/_ErrorHandling", message);
            }

            var result = _magService.Add(model.Article, User.Identity.Name);
            if (!result.IsOk())
            {
                message = String.Format("Hiba a hozzáadás során: {0} - {1}", result.Status, result.Message);
                return PartialView("Layout/_ErrorHandling", message);
            }

            return PartialView("Layout/_SuccessHandling", message);
        }

        public IActionResult DeleteArticle(int id)
        {
            var magazines = _magService.DeleteArticle(id);
            if (magazines.IsOk())
                return PartialView("Layout/_SuccessHandling", "Sikeresen törölve");

            return PartialView("Layout/_ErrorHandling", string.Format("Hiba a törlés során ({0} : {1})", magazines.Status, magazines.Message));
        }

        public IActionResult EditArticle(int id)
        {
            var article = _magService.GetArticleById(id);
            if (!article.IsOk())
                return PartialView("Layout/_ErrorHandling", string.Format("Hiba a törlés során ({0} : {1})", article.Status, article.Message));

            var tags = _magService.GetAllTags();
            if (!tags.IsOk())
                return PartialView("Layout/_ErrorHandling", string.Format("Hiba a törlés során ({0} : {1})", tags.Status, tags.Message));

            var articleVM = new ArticleVM();
            articleVM.Article = article.Data;
            articleVM.Tags = tags.Data;

            return PartialView("Partials/_EditArticle", articleVM);
        }
        #endregion

        #region Images
        #endregion

        #region Keywords
        public IActionResult KeywordList()
        {
            //var result = _magService.GetAllTags();
            return View();
        }

        public IActionResult KeywordGridList()
        {
            var result = _magService.GetAllTags();
            return PartialView("Partials/_KeywordGrid", result.Data);
        }

        public IActionResult AddKeyword(Tag model)
        {
            var result = _magService.Add(model);
            if (result.IsOk())
                return PartialView("Layout/_SuccessHandling", "Sikeresen hozzáadva");

            return PartialView("Layout/_ErrorHandling", String.Format("Hiba a hozzáadás során ({0}:{1})", result.Status, result.Message));
        }

        public IActionResult RemoveTag(int id)
        {
            //TODO: return jsonresult
            var result = _magService.DeleteTag(id);
            if (result.IsOk())
                return Ok("oke");

            TempData["ErrorMessage"] = "Hiba a törlés során";
            return NotFound("hiba");
        }
        #endregion
    }
}