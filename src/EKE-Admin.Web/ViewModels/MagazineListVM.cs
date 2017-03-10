using EKE.Data.Entities.Gyopar;
using System.Collections.Generic;

namespace EKE_Admin.Web.ViewModels
{
    public class MagazineListVM
    {
        public List<Magazine> Magazines { get; set; }
        public List<MagazineCategory> MagazineCategories { get; set; }
    }
}
