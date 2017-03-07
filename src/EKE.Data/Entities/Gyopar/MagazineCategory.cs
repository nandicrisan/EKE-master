using EKE.Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EKE.Data.Entities.Gyopar
{
    public class MagazineCategory : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string PublishedBy { get; set; }
        public virtual ICollection<Magazine> Magazines { get; set; }
    }
}
