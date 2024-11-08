using Ecommerce.Helper;
using Ecommerce.Interfaces;
using Ecommerce.Models;

namespace Ecommerce.Service
{
    public class GenericsService<T> : IGenericService<T> where T: class
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<T>> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.GetRepository<T>().GetByIdAsync(id);
            return new ApiResponse<T>(result);
        }

        public async Task<ApiResponse<IEnumerable<T>>> GetAllAsync()
        {
            var result = await _unitOfWork.GetRepository<T>().GetAllAsync();
            return new ApiResponse<IEnumerable<T>>(result);
        }

        public async Task<ApiResponse<IEnumerable<T>>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _unitOfWork.GetRepository<T>().FindAsync(predicate);
            return new ApiResponse<IEnumerable<T>>(result);
        }

        public async Task<ApiResponse<T>> AddAsync(T entity)
        {
            await _unitOfWork.GetRepository<T>().AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return new ApiResponse<T>(entity);
        }

        public async Task<ApiResponse<T>> Update(T entity)
        {
            _unitOfWork.GetRepository<T>().Update(entity);
            await _unitOfWork.CompleteAsync();
            return new ApiResponse<T>(entity);
        }

        public async Task<ApiResponse<T>> Delete(T entity)
        {
            _unitOfWork.GetRepository<T>().Delete(entity);
            await _unitOfWork.CompleteAsync();
            return new ApiResponse<T>(entity);
        }
    }
}
