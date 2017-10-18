using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKE_Admin.Web.ViewModels
{
    public class HomeVM
    {
        public int GY_Articles { get; set; }
        public int GY_Magazines { get; set; }
        public int GY_Orders { get; set; }
        public int GY_MediaElements { get; set; }

        public int M_Elements { get; set; }
        public int M_Category { get; set; }
        public int M_Tags { get; set; }
        public int M_MediaElements { get; set; }
        public HomeVM()
        {
            GY_Articles = 0;
            GY_Magazines = 0;
            GY_Orders = 0;
            GY_MediaElements = 0;
            M_Elements = 0;
            M_Category = 0;
            M_Tags = 0;
            M_MediaElements = 0;
        }
    }
}
