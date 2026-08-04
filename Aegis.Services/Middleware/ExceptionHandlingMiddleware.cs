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
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var response = ApiResponse<object>.ErrorResponse("Internal Server Error",
                                  "An unexpected error occurred.",
                                   StatusCodes.Status500InternalServerError);
                                   
                                     
                await httpContext.Response.WriteAsJsonAsync(response);

            }
            
        }



    }
}