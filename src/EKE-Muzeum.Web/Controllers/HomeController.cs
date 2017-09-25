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

        public IActionResult GetElements(int page, string category = "", string keyword = "")
        {
            var result = _museumService.GetByPage(page, category, keyword);
            return PartialView("Partials/_ElementHandler", result.Data);
        }

        public IActionResult Search(string keyword)
        {
            var result = _museumService.Search(keyword);
            return PartialView("Partials/_ElementHandler", result.Data);
        }

        public IActionResult GetElement(int id)
        {
            var result = _museumService.GetElementById(id);
            return PartialView("Partials/_ElementDescription", result.Data);
        }

        public IActionResult NextElement(int id)
        {
            var result = _museumService.GetElementById(id);
            return PartialView("Partials/_ElementDescription", result.Data);
        }

        public IActionResult PrevElement(int id)
        {
            var result = _museumService.GetElementById(id);
            return PartialView("Partials/_ElementDescription", result.Data);
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
