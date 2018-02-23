using EKE.Data.Entities.Gyopar;
using EKE.Data.Repository.Base;

namespace EKE.Data.Repository.Gyopar
{
    public interface IOrderRepository : IEntityBaseRepository<Order>
    {
    }

    public class OrderRepository : EntityBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
