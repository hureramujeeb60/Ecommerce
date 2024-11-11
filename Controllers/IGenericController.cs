using Microsoft.AspNetCore.Mvc;

namespace shoppetApi.Controllers
{
    public interface IGenericController<T> where T : class
    {
        Task<ActionResult<IEnumerable<T>>> GetAllAsync();
        Task<ActionResult<T>> GetByIdAsync(int id);
        Task<ActionResult<T>> AddAsync([FromBody] T entity);
        Task<ActionResult<T>> UpdateAsync(int id, [FromBody] T entity);
        Task<ActionResult<T>> DeleteAsync(int id);

    }
}