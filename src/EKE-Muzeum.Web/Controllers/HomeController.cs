using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EKE.Service.Services.Admin.Muzeum;
using EKE_Muzeum.Web.ViewModels;

namespace EKE_Muzeum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMuseumService _museumService;
        public HomeController(IMuseumService museumService, IMapper mapperService)
        {
            _museumService = museumService;
            _mapper = mapperService;
        }

        public IActionResult Index()
        {
            var result = _museumService.GetSelectedRows();
            var map = _mapper.Map<HomeVM>(result.Data);
            return View(map);
        }

        public IActionResult GetElements(int page, string category = "")
        {
            var result = _museumService.GetByPage(page, category);
            return PartialView("Partials/_ElementHandler", result.Data);
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
    }
}
