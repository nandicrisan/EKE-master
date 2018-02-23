using EKE.Data.Entities.Gyopar;
using EKE.Data.Repository.Base;

namespace EKE.Data.Repository.Gyopar
{
    public interface IMagazineRepository : IEntityBaseRepository<Magazine>
    {
        
    }

    public class MagazineRepository : EntityBaseRepository<Magazine>, IMagazineRepository
    {
        public MagazineRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
