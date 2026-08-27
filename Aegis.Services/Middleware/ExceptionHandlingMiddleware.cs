using Aegis.Services.Services;
using Aegis.Utility.Common;

namespace Aegis.Services.Middleware
{
    public class ExceptionHandlingMiddleware
    {

        public readonly RequestDelegate _next;
    


        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                httpContext.Response.Headers.Remove("x-powered-by");
                httpContext.Response.Headers.Remove("server");
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);

            }

        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = ApiResponse<object>.ErrorResponse("Internal Server Error", "An unexpected error occurred.", StatusCodes.Status500InternalServerError);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return context.Response.WriteAsJsonAsync(response);
        }



    }
}