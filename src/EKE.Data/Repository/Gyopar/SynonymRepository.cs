using EKE.Data.Entities.Gyopar;
using EKE.Data.Repository.Base;

namespace EKE.Data.Repository.Gyopar
{
    public interface ISynonymRepository : IEntityBaseRepository<Synonym>
    {
    }

    public class SynonymRepository : EntityBaseRepository<Synonym>, ISynonymRepository
    {
        public SynonymRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
