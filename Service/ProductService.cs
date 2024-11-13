using Ecommerce.Helper;
using Ecommerce.Models;
using Ecommerce.Interfaces;

namespace Ecommerce.Service
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public ProductService(IUnitOfWork unitOfWork,
            IGenericRepository<Category> categoryRepository,
            AsyncOperationHandler operationHandler) : base(unitOfWork, operationHandler)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<APIResponse<IEnumerable<Product>>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _operationHandler.ExecuteAsync(async () =>
            {
                var products = await _repository.FindAsync(p => p.CategoryId == categoryId);
                if (!products.Any())
                {
                    throw new KeyNotFoundException($"No products found for category ID: {categoryId}");
                }
                return products;
            });
        }

        public async Task<APIResponse<IEnumerable<Product>>> SearchProductAsync(string searchTerm)
        {
            return await _operationHandler.ExecuteAsync(async () =>
            {
                var products = await _repository.FindAsync(p => p.Name.Contains(searchTerm));
                return products;
            });
        }

    }
}
