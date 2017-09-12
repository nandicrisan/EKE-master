using EKE.Data.Entities.Base;
using System.Collections.Generic;

namespace EKE.Data.Entities.Gyopar
{
    public class Synonym : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Main { get; set; }
        public virtual ICollection<Synonym> Synonyms { get; set; }
    }
}
