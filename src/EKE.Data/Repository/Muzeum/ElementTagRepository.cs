using EKE.Data.Entities.Museum;
using EKE.Data.Repository.Base;

namespace EKE.Data.Repository.Muzeum
{
    public interface IElementTagsRepository : IEntityBaseRepository<ElementTag>
    {
    }

    public class ElementTagRepository : EntityBaseRepository<ElementTag>, IElementTagsRepository
    {
        public ElementTagRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
