using EKE.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace EKE.Data.Entities.Gyopar
{
    public class Magazine : IEntityBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Cím megadása kötelező")]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required(ErrorMessage = "Évszám megadása kötelező")]
        public int PublishYear { get; set; }
        [Required(ErrorMessage = "Lapszám megadása kötelező")]
        public string PublishSection { get; set; }
        public virtual MagazineCategory Category { get; set; }
        public virtual Author Author { get; set; }
        public DateTime DateCreated { get; set; }
        public string PublishedBy { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<MagazineTag> MagazineTags { get; set; }
        public virtual ICollection<MediaElement> MediaElements { get; set; }
        [NotMapped]
        public IFormFile Files { get; set; }
        public int OrderNo { get; set; }
    }
}
