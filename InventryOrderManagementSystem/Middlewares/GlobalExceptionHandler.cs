namespace InventryOrderManagementSystem.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = new
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "An unexpected error occurred.",
                Details = exception.Message + exception.InnerException
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)result.StatusCode;
            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
