using Ecommerce.Helper;
using Ecommerce.Interfaces;
using Ecommerce.Models;

namespace Ecommerce.Service
{
    public class GenericsService<T> : IGenericService<T> where T: class
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericsService(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }

        public Task<ApiResponse<T>> AddAsync(T entity)
        {
            try
            {

            }
        }
    }
}
