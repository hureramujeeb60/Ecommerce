using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface IOrderRepository: IGenericRepository<Order>
    {
        Task<Order> GetOrderWithProduct(int orderId);
    }
}
