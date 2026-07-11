

namespace Aegis.Utility.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public T Data { get; set; }

        public object? Errors { get; set; }

        public int StatusCode { get; set; }

        public DateTime dateTime { get; set; } = DateTime.Now;

        public ApiResponse(bool success, string message, T data, object errors = null, int statusCode = 200)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
            StatusCode = statusCode;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message, int statusCode = 200)
        {
            return new ApiResponse<T>(true, message, data, null, statusCode);
        }

        public static ApiResponse<T> ErrorResponse(string message, object error = null, int statusCode = 404)
        {
            return new ApiResponse<T>(false, message, default, error, statusCode);

        }



    }


}