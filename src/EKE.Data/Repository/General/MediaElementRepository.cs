using EKE.Data.Entities.Gyopar;
using EKE.Data.Repository.Base;

namespace EKE.Data.Repository.General
{
    public interface IMediaElementRepository : IEntityBaseRepository<MediaElement>
    {
    }

    public class MediaElementRepository : EntityBaseRepository<MediaElement>, IMediaElementRepository
    {
        public MediaElementRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
