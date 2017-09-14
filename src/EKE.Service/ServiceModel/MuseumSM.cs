using EKE.Data.Entities.Museum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKE.Service.ServiceModel
{
    public class MuseumSM
    {
        public Element Element { get; set; }
        public ICollection<IFormFile> Files { get; set; }
        public int SelectedCategoryId { get; set; }
        public string Publisher { get; set; }
    }
}
