using EKE.Data.Entities.Gyopar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKE_Admin.Web.ViewModels
{
    public class MagazineListVM
    {
        public List<Magazine> Magazines { get; set; }
        public List<MagazineCategory> MagazineCategories { get; set; }
    }
}
