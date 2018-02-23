using EKE.Data.Entities.Museum;
using EKE.Data.Repository.Base;

namespace EKE.Data.Repository.Muzeum
{
    public interface IElementCategoryRepository : IEntityBaseRepository<ElementCategory>
    {
    }

    public class ElementCategoryRepository : EntityBaseRepository<ElementCategory>, IElementCategoryRepository
    {
        public ElementCategoryRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
