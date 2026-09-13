using Adveshta.Model.Auth;
using Adveshta.Model.DTO.Auth;
using Adveshta.Model.EmployeeModels;
using Adveshta.Utility.Common;

namespace Adveshta.Services.Services.Interfaces
{
    public interface IAuthService
    {

        Task<ApiResponse<object>> RegisterAsync(RegisterDto model);

        Task<ApiResponse<object>> LoginAsync(LoginDto model);

        Task<ApiResponse<object>> GetWorkSpacesAsync(string userId);

        Task<ApiResponse<object>> SelectWorkSpacesAsync(Guid workspaceId,ApplicationUser user);

        Task<ApiResponse<object>> RefreshToken(string refreshToken);

        Task<ApiResponse<object>> Profile();
    }
}