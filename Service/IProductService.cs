using Ecommerce.Models;
using Ecommerce.Helper;


namespace Ecommerce.Service
{
    public interface IProductService: IGenericService<Product>
    {
        Task<APIResponse<IEnumerable<Product>>> GetProductsByCategoryAsync(int categoryId);
        Task<APIResponse<IEnumerable<Product>>> SearchProductAsync(string searchTerm);
    }
}
