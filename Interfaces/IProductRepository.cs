using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsWithCategory(int categoryId);
    }
}
