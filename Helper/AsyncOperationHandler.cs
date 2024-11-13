namespace Ecommerce.Helper
{
    public class AsyncOperationHandler
    {

        public async Task<APIResponse<TResult>> ExecuteAsync<TResult>(Func<Task<TResult>> action, string successMessage = null)
        {
            try
            {
                var result = await action();
                return new APIResponse<TResult>
                {
                    Success = true,
                    Message = successMessage ?? MessageHelper.Success(typeof(TResult).Name, "retrieved"),
                    Data = result
                };
            }
            catch (KeyNotFoundException knfEx)
            {
                return new APIResponse<TResult>
                {
                    Success = false,
                    Message = knfEx.Message
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<TResult>
                {
                    Success = false,
                    Message = MessageHelper.Exception(typeof(TResult).Name, "operation", ex.Message)
                };
            }
        }
    }
}
