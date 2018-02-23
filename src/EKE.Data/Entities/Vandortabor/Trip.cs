using System.Collections.Generic;
using EKE.Data.Entities.Base;
using EKE.Data.Entities.Enums;

namespace EKE.Data.Entities.Vandortabor
{
    public class Trip : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<VtTripAttributes> Attributes { get; set; }
        public VtTripDifficulty Difficulty { get; set; }
        public int Length { get; set; }
    }
}
