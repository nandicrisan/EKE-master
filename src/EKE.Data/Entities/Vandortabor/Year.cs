using System.Collections.Generic;
using EKE.Data.Entities.Base;

namespace EKE.Data.Entities.Vandortabor
{
    public class Year : IEntityBase
    {
        public int Id { get; set; }
        public int Season { get; set; }
        public Dictionary<string,bool> Days { get; set; }
        public bool IsActive { get; set; }
    }
}
