using EKE.Data.Entities.Gyopar;
using EKE.Data.Repository.Base;

namespace EKE.Data.Repository.Gyopar
{
    public interface ITagRepository : IEntityBaseRepository<Tag>
    {
    }

    public class TagRepository : EntityBaseRepository<Tag>, ITagRepository
    {
        public TagRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
