using Ecommerce.Helper;
using Ecommerce.Interfaces;


namespace Ecommerce.Service
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IGenericRepository<T> _repository;
        protected readonly AsyncOperationHandler _operationHandler;

        public GenericService(IUnitOfWork unitOfWork, AsyncOperationHandler operationHandler)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<T>();
            _operationHandler = operationHandler;
        }

        public async Task<APIResponse<T>> GetByIdAsync(int id)
        {
            return await _operationHandler.ExecuteAsync(async ()=>
            {
                var result = await _repository.GetByIdAsync(id);
                return result ?? throw new KeyNotFoundException(MessageHelper.NotFound(typeof(T).Name));
            });
        }

        public async Task<APIResponse<IEnumerable<T>>> GetAllAsync()
        {
            return await _operationHandler.ExecuteAsync(async () =>
            {
                var result = await _repository.GetAllAsync();
                return result ?? Enumerable.Empty<T>();
            }, "Fetched successfully.");
        }

        public async Task<APIResponse<T>> AddAsync(T entity)
        {
            return await _operationHandler.ExecuteAsync(async () =>
            {
                await _repository.AddAsync(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }, MessageHelper.Success(typeof(T).Name, "Created"));
        }

        public async Task<APIResponse<T>> Update(int id, T entity)
        {
            return await _operationHandler.ExecuteAsync(async () =>
            {
                var existingEntity = await _repository.GetByIdAsync(id);
                if (existingEntity == null)
                {
                    throw new KeyNotFoundException(MessageHelper.NotFound(typeof(T).Name));
                }
                await _repository.Update(id,entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }, MessageHelper.Success(typeof(T).Name, "Updated"));
        }

        public async Task<APIResponse<T>> Delete(int id)
        {
            return await _operationHandler.ExecuteAsync(async () =>
            {
                var existingEntity = await _repository.GetByIdAsync(id);
                if (existingEntity == null)
                {
                    throw new KeyNotFoundException(MessageHelper.NotFound(typeof(T).Name));
                }
                await _repository.Delete(id);
                await _unitOfWork.CompleteAsync();
                return existingEntity;
            }, MessageHelper.Success(typeof(T).Name, "Deleted")); 
        }
    }
}
