using Ecommerce.Interfaces;
using Ecommerce.Models;

namespace Ecommerce.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        public UnitOfWork(ApplicationDbContext context,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IOrderItemRepository orderItemRepository
            )
        {
            _context = context;
            Customers = customerRepository;
            Orders = orderRepository;
            Products = productRepository;
            Categories = categoryRepository;
            OrderItems = orderItemRepository;
        }

        public ICustomerRepository Customers { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IProductRepository Products { get; private set; }
        public IOrderItemRepository OrderItems { get; private set; }
        public ICategoryRepository Categories { get; private set; }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repository = new GenericRepository<T>(_context);
                _repositories.TryAdd(typeof(T), repository);
            }

            return (IGenericRepository<T>)_repositories[typeof(T)];
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
