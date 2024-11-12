using Ecommerce.Helper;
using Ecommerce.Interfaces;


namespace Ecommerce.Service
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _repository;

        public GenericService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<T>();
        }

        public async Task<APIResponse<T>> GetByIdAsync(int id)
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

        public async Task<APIResponse<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync();
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

        public async Task<APIResponse<T>> AddAsync(T entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                await _unitOfWork.CompleteAsync();
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

        public async Task<APIResponse<T>> Update(int id, T entity)
        {
            try
            {
                var data = await _repository.GetByIdAsync(id);
                if (data == null)
                {
                    return new APIResponse<T>
                    {
                        Success = false,
                        Message = MessageHelper.NotFound(typeof(T).Name)
                    };
                }

                _repository.Update(id, entity);
                await _unitOfWork.CompleteAsync();
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

        public async Task<APIResponse<T>> Delete(int id)
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
                await _repository.Delete(id);
                await _unitOfWork.CompleteAsync();
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
