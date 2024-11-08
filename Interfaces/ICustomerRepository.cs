using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface ICustomerRepository: IGenericRespository<Customer>
    {
        Task<Customer> GetCustomerWithOrder(int customerId);
    }
}
