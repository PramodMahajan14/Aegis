using Aegis.Model.DTO.Auth;
using Aegis.Utility.Common;

namespace Aegis.Services.Services.Interfaces
{
    public interface IAuthService
    {

        Task<ApiResponse<object>> RegisterAsync(RegisterDto model);

        Task<ApiResponse<object>> LoginAsync(LoginDto model);

        Task<ApiResponse<object>> Profile();
    }
}