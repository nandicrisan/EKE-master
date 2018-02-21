using System.Collections.Generic;
using EKE.Data.Entities.Base;

namespace EKE.Data.Entities.Vandortabor
{
    public class VtYear : IEntityBase
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public Dictionary<string,bool> Days { get; set; }
        public bool IsActive { get; set; }
    }
}
