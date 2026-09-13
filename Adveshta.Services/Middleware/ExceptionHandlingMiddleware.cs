using Adveshta.Services.Services;
using Adveshta.Utility.Common;

namespace Adveshta.Services.Middleware
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

   Console.WriteLine(exception);

            var response = ApiResponse<object>.ErrorResponse("Internal Server Error", "An unexpected error occurred.", StatusCodes.Status500InternalServerError);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return context.Response.WriteAsJsonAsync(response);
        }



    }
}