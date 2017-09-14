using EKE.Data.Entities.Museum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EKE_Admin.Web.ViewModels
{
    public class MuseumVM
    {
        public Element Element { get; set; }
        public List<ElementCategory> Categories { get; set; }
        public ICollection<IFormFile> Files { get; set; }
        [Required]
        public int SelectedCategoryId { get; set; }
        public MuseumVM()
        {
            Element = new Element();
            Categories = new List<ElementCategory>();
        }
    }
}
