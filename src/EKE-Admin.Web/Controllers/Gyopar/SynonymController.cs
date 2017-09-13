using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EKE.Service.Services.Admin;
using AutoMapper;
using EKE.Data.Entities.Gyopar;
using EKE_Admin.Web.ViewModels;
using EKE.Service.ServiceModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EKE_Admin.Web.Controllers
{
    [Authorize(Roles = "superadmin,gyopar")]
    [AutoValidateAntiforgeryToken]
    public class SynonymController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMagazineService _magService;

        public SynonymController(IMagazineService magazineService, IMapper mapperService)
        {
            _magService = magazineService;
            _mapper = mapperService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SynonymListGrid()
        {
            var synonyms = _magService.GetAllSynonyms();
            if (!synonyms.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", synonyms.Status, synonyms.Message);
                return PartialView("Partials/_SynonymListGrid", new List<Synonym>());
            }

            // Only grid string query values will be visible here.
            return PartialView("Partials/_SynonymListGrid", synonyms.Data);
        }

        public IActionResult AddSynonym(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                var synonym = _magService.AddSynonym(text);
                if (synonym.IsOk())
                    return Json("");

                return PartialView("Layout/_ErrorHandling", string.Format("Hiba a törlés során ({0} : {1})", synonym.Status, synonym.Message));
            }
            return PartialView("Layout/_ErrorHandling", "Hiba a törlés során!");
        }

        public IActionResult DeleteSynonym(int id)
        {
            if (id > 0)
            {
                var synonym = _magService.DeleteSynonym(id);
                if (synonym.IsOk())
                    return RedirectToAction("Index");

                TempData["ErrorMessage"] = string.Format("Hiba a törlés során ({0} : {1})", synonym.Status, synonym.Message);
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = string.Format("Hiba a törlés során: Nem létező paraméter");
            return RedirectToAction("Index");
        }

        public IActionResult ConnectSynonym(int id, string text)
        {
            if (id > 0 && !String.IsNullOrEmpty(text))
            {
                var synonym = _magService.ConnectSynonym(id, text);
                if (synonym.IsOk())
                    return RedirectToAction("Index");

                TempData["ErrorMessage"] = string.Format("Hiba a törlés során ({0} : {1})", synonym.Status, synonym.Message);
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = string.Format("Hiba a törlés során: Nem létező paraméter");
            return RedirectToAction("Index");
        }

        public IActionResult UpdateSynonym(XEditVM model)
        {
            var mappedModel = _mapper.Map<XEditSM>(model);
            var synonym = _magService.UpdateSynonym(mappedModel);
            if (synonym.IsOk())
                return RedirectToAction("Index");

            TempData["ErrorMessage"] = string.Format("Hiba a törlés során ({0} : {1})", synonym.Status, synonym.Message);
            return RedirectToAction("Index");
        }
    }
}
