using System.Linq.Expressions;

namespace Ecommerce.Interfaces
{
    public interface IGenericRespository<T> where T : class
    {
        Task<T> GetByIdAsync (int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> AddAsync(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        
    }
}
