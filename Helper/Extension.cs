using Ecommerce.Interfaces;
using Ecommerce.Repositories;
using Ecommerce.Service;

namespace Ecommerce.Helper
{
    public static class Extension
    {
        public static void RegisterServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
        }


        public static void RegisterRepositories(this IServiceCollection Services)
        {
            Services.AddScoped<ICustomerRepository, CustomerRepository>();
            Services.AddScoped<IOrderRepository, OrderRepository>();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<ICategoryRepository, CategoryRepository>();
            Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        }
    }
}
