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
        public HomeController(IMapper mapper, IArticleService articleService)
        {
            _articleService = articleService;
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
        public IActionResult SearchMagazine(ArticleSearch filter)
        {
            var res = _articleService.Get(filter);
            if(!res.IsOk())
                return StatusCode((int)res.Status, res.Message);
            List<ArticleSerchItemVM> vmList =  Mapper.Map<List<Article>, List<ArticleSerchItemVM>>(res.Data);
            var serachResult = new ArticleSerchResultVM
            {
                Result = vmList
            };
            //Get result count
            var count = _articleService.Count(filter);
            if (count.IsOk())
                serachResult.FoundItem = count.Data;
            return Json(serachResult);
        }
    }
}
