using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EKE.Data.DataViewModels;
using AutoMapper;
using EKE.Service.Services.Admin;
using EKE_Gyopar.Web.ViewModels;
using EKE.Data.Entities.Gyopar;

namespace EKE_Gyopar.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IArticleService _articleService;
        private readonly IMagazineService _magazineService;
        public HomeController(IMapper mapper, IArticleService articleService, IMagazineService magazineService)
        {
            _articleService = articleService;
            _magazineService = magazineService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Search(ArticleSearch filter)
        {
            var res = _articleService.Get(filter);
            if (!res.IsOk())
                return StatusCode((int)res.Status, res.Message);
            List<ArticleSerchItemVM> vmList = Mapper.Map<List<Article>, List<ArticleSerchItemVM>>(res.Data);
            var searchResult = new ArticleSerchResultVM { Result = vmList };
            //Get result count
            var count = _articleService.Count(filter);
            if (count.IsOk())
                searchResult.FoundItem = count.Data;
            return PartialView("Partials/_SearchResult", searchResult);
        }

        [HttpGet]
        public IActionResult SearchByMagazineYear(string year)
        {
            var result = _magazineService.GetAllMagazinesBy(x => x.PublishYear == Convert.ToInt32(year));
            if (result.IsOk())
                return PartialView("Partials/_FoundMagazines", result.Data.OrderBy(x => x.PublishSection).ToList());

            return StatusCode((int)result.Status, result.Message);
        }

        [HttpGet]
        public IActionResult GetLastMagazines()
        {
            var res = _magazineService.GetLastMagazines(10);
            if (!res.IsOk()) return StatusCode((int)res.Status, res.Message);
            return PartialView("Partials/_LastMagazines", res.Data);
        }

        [HttpGet]
        public IActionResult GetSelectedArticles()
        {
            var res = _articleService.GetSelected();
            if (!res.IsOk()) return StatusCode((int)res.Status, res.Message);
            return PartialView("Partials/_SelectedArticles", res.Data);
        }

        [HttpGet]
        public IActionResult SearchMagazineById(string magId)
        {
            var result = _magazineService.GetMagazineById(Convert.ToInt32(magId));
            if (result.IsOk())
                return PartialView("Partials/_ArticleList", result.Data);

            return StatusCode((int)result.Status, result.Message);
        }

        [HttpGet]
        public IActionResult SearchArticleById(string slug)
        {
            var result = _magazineService.GetArticleBySlug(slug);
            if (result.IsOk())
                return PartialView("Partials/_Article", result.Data);

            return StatusCode((int)result.Status, result.Message);
        }

        [HttpPost]
        public IActionResult AddOrder(Order model)
        {
            if (ModelState.IsValid)
            {
                //var result = _magazineService.AddOrder(model);
                return null;
            }
            return null;
        }

    }
}
