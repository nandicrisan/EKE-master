using EKE.Data.Entities.Museum;
using EKE.Data.Repository.Base;

namespace EKE.Data.Repository.Muzeum
{
    public interface IElementRepository : IEntityBaseRepository<Element>
    {
    }

    public class ElementRepository : EntityBaseRepository<Element>, IElementRepository
    {
        public ElementRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
