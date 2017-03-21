using EKE.Data.Entities;
using EKE.Data.Entities.Identity.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKE_Admin.Web.ViewModels
{
    public class UserManagementVM
    {
        public List<ApplicationUser> AppUser { get; set; }
        public RegisterViewModel RegisterVM { get; set; }
        public List<ApplicationRole> Roles { get; set; }
    }
}
