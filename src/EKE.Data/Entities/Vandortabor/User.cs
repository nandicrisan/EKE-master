using EKE.Data.Entities.Base;

namespace EKE.Data.Entities.Vandortabor
{
    public class User : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Birthdate { get; set; }
    }
}
