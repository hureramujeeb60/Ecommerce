using Ecommerce.Helper;
using System.Linq.Expressions;

namespace Ecommerce.Service
{
    public interface IGenericService<T> where T : class
    {
        Task<ApiResponse<T>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<T>>> GetAllAsync();
        Task<ApiResponse<T>> AddAsync(T entity);
        Task<ApiResponse<T>> Update(T entity);
        Task<ApiResponse<T>> Delete(T entity);
    }
}
