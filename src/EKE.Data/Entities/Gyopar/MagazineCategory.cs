using EKE.Data.Entities.Base;
using System.Collections.Generic;

namespace EKE.Data.Entities.Gyopar
{
    public class MagazineCategory : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Magazine> Magazines { get; set; }
    }
}
