using EKE.Data.Entities.Gyopar;
using EKE.Data.Repository.Base;

namespace EKE.Data.Repository.Gyopar
{
    public interface IMagazineCategoryRepository : IEntityBaseRepository<MagazineCategory>
    {
        
    }

    public class MagazineCategoryRepository : EntityBaseRepository<MagazineCategory>, IMagazineCategoryRepository
    {
        public MagazineCategoryRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
