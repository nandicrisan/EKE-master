using EKE.Data.Entities.Gyopar;
using EKE.Data.Repository.Base;

namespace EKE.Data.Repository.Gyopar
{
    public interface IArticleRepository : IEntityBaseRepository<Article>
    {
        
    }

    public class ArticleRepository : EntityBaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
