using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface ICustomerRepository: IGenericRepository<Customer>
    {
        Task<Customer> GetCustomerWithOrder(int customerId);
    }
}
