namespace Ecommerce.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IOrderItemRepository OrderItems { get; }


        Task<int> CompleteAsync();
    }
}
