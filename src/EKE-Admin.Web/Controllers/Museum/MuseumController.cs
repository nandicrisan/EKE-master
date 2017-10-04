using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EKE.Service.Services.Admin;
using AutoMapper;
using EKE.Data.Entities.Gyopar;
using EKE.Service.Services.Admin.Muzeum;
using EKE.Data.Entities.Museum;
using EKE_Admin.Web.ViewModels;
using EKE.Service.ServiceModel;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EKE_Admin.Web.Controllers
{
    [Authorize(Roles = "superadmin,museum")]
    [AutoValidateAntiforgeryToken]
    public class MuseumController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMuseumService _museumService;
        public MuseumController(IMuseumService museumService, IMapper mapperService)
        {
            _museumService = museumService;
            _mapper = mapperService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult Category()
        {
            var model = new MuseumVM();
            var categories = _museumService.GetAllElementCategories();
            if (!categories.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", categories.Status, categories.Message);
                return View();
            }

            model.Categories = categories.Data;
            return View(model);
        }

        public IActionResult ElementListGrid()
        {
            var elements = _museumService.GetAllElements();
            if (!elements.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", elements.Status, elements.Message);
                return PartialView("Partials/_ElementListGrid", new List<Order>());
            }

            // Only grid string query values will be visible here.
            return PartialView("Partials/_ElementListGrid", elements.Data.OrderByDescending(x => x.Id));
        }

        public IActionResult ElementCategoryListGrid()
        {
            var elements = _museumService.GetAllElementCategories();
            if (!elements.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", elements.Status, elements.Message);
                return PartialView("Partials/_ElementCategoryListGrid", new List<Order>());
            }

            // Only grid string query values will be visible here.
            return PartialView("Partials/_ElementCategoryListGrid", elements.Data);
        }

        public IActionResult DeleteElement(int id)
        {
            if (id > 0)
            {
                var element = _museumService.DeleteElement(id);
                if (element.IsOk())
                    return RedirectToAction("Index");

                TempData["ErrorMessage"] = string.Format("Hiba a törlés során ({0} : {1})", element.Status, element.Message);
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = string.Format("Hiba a törlés során: Nem létező paraméter");
            return RedirectToAction("Index");
        }

        public IActionResult DeleteElementCategory(int id)
        {
            if (id > 0)
            {
                var elementCat = _museumService.DeleteElementCategory(id);
                if (elementCat.IsOk())
                    return RedirectToAction("Category");

                TempData["ErrorMessage"] = string.Format("Hiba a törlés során ({0} : {1})", elementCat.Status, elementCat.Message);
                return RedirectToAction("Category");
            }
            TempData["ErrorMessage"] = string.Format("Hiba a törlés során: Nem létező paraméter");
            return RedirectToAction("Category");
        }

        public IActionResult AddCategory(string text, int parent)
        {
            if (!String.IsNullOrEmpty(text))
            {
                var category = _museumService.AddElementCategory(text, User.Identity.Name, parent);
                if (category.IsOk())
                    return Json("");

                return PartialView("Layout/_ErrorHandling", string.Format("Hiba a törlés során ({0} : {1})", category.Status, category.Message));
            }
            return PartialView("Layout/_ErrorHandling", "Hiba a törlés során!");
        }

        public IActionResult CreateElemPartial()
        {
            var category = _museumService.GetAllElementCategories();
            if (!category.IsOk()) return PartialView("Layout/_ErrorHandling", string.Format("Hiba a lekérés során ({0} : {1})", category.Status, category.Message));

            var tags = _museumService.GetAllTags();
            if (!tags.IsOk()) return PartialView("Layout/_ErrorHandling", string.Format("Hiba a lekérés során ({0} : {1})", category.Status, category.Message));

            var model = new MuseumVM();
            model.Categories = category.Data;
            model.Tags = tags.Data;

            return PartialView("Partials/_AddElement", model);
        }

        public IActionResult AddElement(MuseumVM model)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<MuseumSM>(model);
                map.Publisher = User.Identity.Name;
                var result = _museumService.AddElement(map);
                if (result.IsOk()) return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = string.Format("Hiba a hozzáadás során: Nem létező paraméter");
            return RedirectToAction("Index");
        }

        public IActionResult ElementTagsListGrid()
        {
            var elements = _museumService.GetAllTags();
            if (!elements.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", elements.Status, elements.Message);
                return PartialView("Partials/_ElementTagsListGrid", new List<Order>());
            }

            // Only grid string query values will be visible here.
            return PartialView("Partials/_ElementTagsListGrid", elements.Data);
        }

        public IActionResult Tag()
        {
            return View();
        }

        public IActionResult AddTag(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                var category = _museumService.AddElementTag(text, User.Identity.Name);
                if (category.IsOk())
                    return Json("");

                return PartialView("Layout/_ErrorHandling", string.Format("Hiba a törlés során ({0} : {1})", category.Status, category.Message));
            }
            return PartialView("Layout/_ErrorHandling", "Hiba a törlés során!");
        }

        public IActionResult DeleteElementTag(int id)
        {
            if (id > 0)
            {
                var elementCat = _museumService.DeleteElementTag(id);
                if (elementCat.IsOk())
                    return RedirectToAction("Tag");

                TempData["ErrorMessage"] = string.Format("Hiba a törlés során ({0} : {1})", elementCat.Status, elementCat.Message);
                return RedirectToAction("Tag");
            }
            TempData["ErrorMessage"] = string.Format("Hiba a törlés során: Nem létező paraméter");
            return RedirectToAction("Tag");
        }

        public IActionResult GetCategories()
        {
            var model = new List<XEditSelect>();
            var categories = _museumService.GetAllElementCategories();
            if (!categories.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", categories.Status, categories.Message);
                return View();
            }

            model = Mapper.Map<List<XEditSelect>>(categories.Data);
            return Json(model);
        }

        [HttpPost]
        public IActionResult UploadCover(ICollection<IFormFile> files, int id)
        {
            var result = _museumService.UpdateCover(files, id);
            if (result.IsOk()) return Json(200);
            return Json(result.Status);
        }

        #region XEdit
        [HttpPost]
        public IActionResult Update(XEditVM model)
        {
            var mappedModel = _mapper.Map<XEditSM>(model);
            var result = _museumService.Update(mappedModel);
            if (result.IsOk()) return Json(200);
            return Json(result.Status);
        }
        #endregion
    }
}
