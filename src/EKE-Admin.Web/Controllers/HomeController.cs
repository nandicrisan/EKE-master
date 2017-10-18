using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EKE.Service.Services.Admin;
using EKE.Service.Services;
using EKE.Service.Services.Admin.Muzeum;
using EKE_Admin.Web.ViewModels;

namespace EKE_Admin.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMagazineService _magService;
        private readonly IArticleService _articleService;
        private readonly IMuseumService _museumService;

        public HomeController(
            IMagazineService magazineService,
            IArticleService articleService,
            IMuseumService museumService,
            IMapper mapperService)
        {
            _magService = magazineService;
            _articleService = articleService;
            _museumService = museumService;
            _mapper = mapperService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var homeVM = new HomeVM();
            var gyoparResult = _magService.GetAllMagazinesIncluding();
            if (gyoparResult.IsOk())
            {
                homeVM.GY_Magazines = gyoparResult.Data.Count;
                homeVM.GY_Articles = gyoparResult.Data.SelectMany(x => x.Articles).Count();
                homeVM.GY_MediaElements = gyoparResult.Data.SelectMany(x => x.MediaElements.Where(y => y.Scope == EKE.Data.Entities.Enums.MediaTypesScope.Cover)).Count();
            };

            var museumResult = _museumService.GetAllElements();
            if (museumResult.IsOk())
            {
                homeVM.M_Elements = museumResult.Data.Count;
                homeVM.M_MediaElements = museumResult.Data.SelectMany(x => x.MediaElement.Where(y => y.Scope == EKE.Data.Entities.Enums.MediaTypesScope.Museum)).Count();
                homeVM.M_Tags = museumResult.Data.SelectMany(x => x.Tags).Count();
            };
            return View(homeVM);
        }

        [Authorize(Roles = "admin")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
    }
}
