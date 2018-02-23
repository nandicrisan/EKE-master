using EKE.Data.Entities.Gyopar;
using EKE.Data.Repository.Base;

namespace EKE.Data.Repository.Gyopar
{
    public interface IAuthorRepository : IEntityBaseRepository<Author>
    {
        
    }

    public class AuthorRepository : EntityBaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
