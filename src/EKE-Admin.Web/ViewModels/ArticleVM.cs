using EKE.Data.Entities.Gyopar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKE_Admin.Web.ViewModels
{
    public class ArticleVM
    {
        public Article Article { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
