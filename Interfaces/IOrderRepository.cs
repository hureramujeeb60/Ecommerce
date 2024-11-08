using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface IOrderRepository: IGenericRespository<Order>
    {
        Task<Order> GetOrderWithProduct(int orderId);
    }
}
