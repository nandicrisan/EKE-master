using EKE.Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EKE.Data.Entities
{
    public class Author : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<MediaElement> MediaElements { get; set; }
        public ICollection<Magazine> Magazines { get; set; }
    }
}
