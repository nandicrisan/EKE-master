using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKE_Admin.Web.Controllers
{
    [Authorize(Roles = "superadmin,gyopar")]
    public class MagazineController : Controller
    {
        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            return View();
        }
    }
}
