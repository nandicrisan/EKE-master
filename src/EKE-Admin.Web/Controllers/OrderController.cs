using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EKE.Service.Services.Admin;
using AutoMapper;
using EKE.Data.Entities.Gyopar;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EKE_Admin.Web.Controllers
{
    [Authorize(Roles = "superadmin,gyopar")]
    [AutoValidateAntiforgeryToken]
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMagazineService _magService;
        public OrderController(IMagazineService magazineService, IMapper mapperService)
        {
            _magService = magazineService;
            _mapper = mapperService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderListGrid()
        {
            var magazines = _magService.GetAllOrders();
            if (!magazines.IsOk())
            {
                TempData["ErrorMessage"] = string.Format("Hiba a lekérés során ({0} : {1})", magazines.Status, magazines.Message);
                return PartialView("Partials/_OrderListGrid", new List<Order>());
            }

            // Only grid string query values will be visible here.
            return PartialView("Partials/_OrderListGrid", magazines.Data);
        }

        public IActionResult DeleteOrder(int id)
        {
            if (id > 0)
            {
                var magazines = _magService.DeleteOrder(id);
                if (magazines.IsOk())
                    return RedirectToAction("Index");

                TempData["ErrorMessage"] = string.Format("Hiba a törlés során ({0} : {1})", magazines.Status, magazines.Message);
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = string.Format("Hiba a törlés során: Nem létező paraméter");
            return RedirectToAction("Index");
        }
    }
}
