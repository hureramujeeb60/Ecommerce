using Ecommerce.Helper;
using System.Linq.Expressions;

namespace Ecommerce.Service
{
    public interface IGenericService<T> where T : class
    {
        Task<APIResponse<T>> GetByIdAsync(int id);
        Task<APIResponse<IEnumerable<T>>> GetAllAsync();
        Task<APIResponse<T>> AddAsync(T entity);
        Task<APIResponse<T>> Update(int id, T entity);
        Task<APIResponse<T>> Delete(int id);
    }
}
