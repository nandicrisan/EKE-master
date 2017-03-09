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
using System.Linq.Expressions;

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
            var articles = _magService.GetAllArticles();
            if (articles.IsOk())
                return View(articles.Data);

            TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", articles.Status, articles.StatusText);
            return View(new List<Article>());
        }

        #region Magazine
        public IActionResult MagazineList()
        {
            var magazineCategories = _magService.GetAllMagazineCategories();
            if (!magazineCategories.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", magazineCategories.Status, magazineCategories.Message);
                return View(new List<Article>());
            }

            var magazines = _magService.GetAllMagazines();
            if (!magazines.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", magazines.Status, magazines.Message);
                return View(new List<Article>());
            }

            MagazineListVM viewmodel = _mapper.Map<MagazineListVM>(magazineCategories.Data).Map(magazines.Data);
            return View(viewmodel);
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
            var predicate = PredicateBuilder.New<Article>();
            if (format != 0)
                predicate.And(x => x.Magazine.Category.Id == format);

            if (year != 0)
                predicate.And(x => x.Magazine.PublishYear == year);

            if (section != 0)
                predicate.And(x => x.Magazine.PublishSection.Contains(String.Format("{0}",section)));

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
            var magazineCategory = _magService.GetMagazineCategoryById(format);

            if (!magazineCategory.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", magazineCategory.Status, magazineCategory.Message);
                return PartialView("Partials/_AddArticle");
            }

            var magazine = new Magazine();
            magazine.Category = magazineCategory.Data;
            magazine.PublishYear = year;
            magazine.PublishSection = String.Format("{0}",section);

            var model = new Article();
            model.Magazine = magazine;
            // Only grid string query values will be visible here.
            return PartialView("Partials/_AddArticle", model);
        }

        [HttpPost]
        public IActionResult AddArticle(Article model)
        {
            return null;
        }
        #endregion
    }
}
