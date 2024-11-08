

namespace Ecommerce.Service
{
    public class ProductService : IGenericService<Product>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAsync(Product product)
        {
            if (product.Price < 1.00m)
            {
                throw new ArgumentException("Product price must be at least 1.00.");
            }

            await _unitOfWork.Products.AddAsync(product);

            await _unitOfWork.CompleteAsync();

            return true;

        }
}