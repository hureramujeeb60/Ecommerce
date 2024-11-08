using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface IProductRepository: IGenericRespository<Product>
    {
        Task<IEnumerable<Product>> GetProductsWithCategory(int categoryId);
    }
}
