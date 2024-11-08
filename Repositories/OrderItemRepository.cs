using Ecommerce.Data;
using Ecommerce.Interfaces;
using Ecommerce.Models;

namespace Ecommerce.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDbContext context) : base(context) { }

    }
}
