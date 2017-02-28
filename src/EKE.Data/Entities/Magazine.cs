using EKE.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EKE.Data.Entities
{
    public class Magazine : IEntityBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "")]
        public string Title { get; set; }
        [Required(ErrorMessage = "")]
        public string Slug { get; set; }
        public DateTime Date { get; set; }
        public virtual MagazineCategory Category { get; set; }
        public virtual Author Author { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<MagazinTag> MagazinTags { get; set; }
        public virtual ICollection<MediaElement> MediaElements { get; set; }
    }
}
