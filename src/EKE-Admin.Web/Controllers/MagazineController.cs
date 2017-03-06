using EKE.Data.Entities.Gyopar;
using EKE.Service.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EKE_Admin.Web.Controllers
{
    [Authorize(Roles = "superadmin,gyopar")]
    public class MagazineController : Controller
    {
        private readonly IMagazineService _magService;
        public MagazineController(IMagazineService magazineService)
        {
            _magService = magazineService;
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            var articles = _magService.GetAllArticles();
            if (articles.IsOk())
                return View(articles.Data);

            ViewData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0 : {1}})", articles.Status, articles.Message);
            return View(new List<Article>());
        }
    }
}
