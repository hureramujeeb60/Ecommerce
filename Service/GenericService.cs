using Ecommerce.Helper;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using System.Linq.Expressions;

namespace Ecommerce.Service
{
    public class GenericsService<T> : IGenericService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _repository;

        public GenericsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<T>();
        }

        public async Task<ApiResponse<T>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                if (result == null)
                {
                    return new APIResponse<T>
                    {
                        Success = false,
                        Message = MessageHelper.NotFound(typeof(T).Name)
                    };
                }
                return new APIResponse<T>
                {
                    Success = true,
                    Message = MessageHelper.Success(typeof(T).Name, "retrieved"),
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<T>
                {
                    Success = false,
                    Message = MessageHelper.Exception(typeof(T).Name, "retrieving", ex.Message)
                };
            }
        }

        public async Task<ApiResponse<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                var result = await _genericRepository.GetAll();
                if (result == null)
                {
                    return new APIResponse<IEnumerable<T>>()
                    {
                        Success = false,
                        Message = MessageHelper.NotFound(typeof(T).Name),
                    };
                }
                return new APIResponse<IEnumerable<T>>()
                {
                    Success = true,
                    Message = MessageHelper.Success(typeof(T).Name, "fetched"),
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<T>>
                {
                    Success = false,
                    Message = MessageHelper.Exception(typeof(T).Name, "fetching", ex.Message)
                };
            }
        }

        public async Task<ApiResponse<T>> AddAsync(T entity)
        {
            try
            {
                await _genericRepository.Add(entity);
                await _unitOfWork.SaveAsync();
                return new APIResponse<T>
                {
                    Success = true,
                    Message = MessageHelper.Success(typeof(T).Name, "created"),
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<T>
                {
                    Success = false,
                    Message = MessageHelper.Exception(typeof(T).Name, "creating", ex.Message),
                };
            }
        }

        public async Task<ApiResponse<T>> Update(T entity)
        {
            try
            {
                var data = await _genericRepository.GetById(id);
                if (data == null)
                {
                    return new APIResponse<T>
                    {
                        Success = false,
                        Message = MessageHelper.NotFound(typeof(T).Name)
                    };
                }

                await _genericRepository.Update(id, entity);
                await _unitOfWork.SaveAsync();
                return new APIResponse<T>
                {
                    Success = true,
                    Message = MessageHelper.Success(typeof(T).Name, "updated")
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<T>
                {
                    Success = false,
                    Message = MessageHelper.Exception(typeof(T).Name, "updating", ex.Message)
                };
            }
        }

        public async Task<ApiResponse<T>> Delete(T entity)
        {
            try
            {
                var result = await _genericRepository.GetById(id);
                if (result == null)
                {
                    return new APIResponse<T>
                    {
                        Success = false,
                        Message = MessageHelper.NotFound(typeof(T).Name)
                    };
                }
                await _genericRepository.Delete(id);
                await _unitOfWork.SaveAsync();
                return new APIResponse<T>
                {
                    Success = true,
                    Message = MessageHelper.Success(typeof(T).Name, "deleted")
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<T>
                {
                    Success = false,
                    Message = MessageHelper.Exception(typeof(T).Name, "deleting", ex.Message)
                };
            }
        }
    }
}
