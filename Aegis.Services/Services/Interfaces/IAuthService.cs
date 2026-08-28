using Aegis.Model.DTO.Auth;
using Aegis.Model.EmployeeModels;
using Aegis.Utility.Common;

namespace Aegis.Services.Services.Interfaces
{
    public interface IAuthService
    {

        Task<ApiResponse<object>> RegisterAsync(RegisterDto model);

        Task<ApiResponse<object>> LoginAsync(LoginDto model);

        Task<ApiResponse<object>> GetWorkSpacesAsync(string userId);

        Task<ApiResponse<object>> Profile();
    }
}