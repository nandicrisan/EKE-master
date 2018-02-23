using EKE.Data.Entities.Base;
using EKE.Data.Entities.Enums;
using EKE.Data.Entities.Gyopar;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EKE.Data.Entities.Home
{
    public class News : IEntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Slug { get; set; }
        public string PublishedBy { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public NewsCategories Category { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<MediaElement> MediaElements { get; set; }

        [NotMapped]
        public List<IFormFile> Files { get; set; }

        News()
        {
            DateAdded = DateTime.Now;
        }
    }
}
