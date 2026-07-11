
using Aegis.Model.Auth;
using Aegis.Model.DTO.Auth;
using Aegis.Services.Services.Interfaces;
using Aegis.Utility.Common;
using Microsoft.AspNetCore.Identity;

namespace Aegis.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse<object>> RegisterAsync(RegisterDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrWhiteSpace(model.FirstName) || string.IsNullOrWhiteSpace(model.LastName))
            {
                return ApiResponse<object>.ErrorResponse("Invalid request", "FirstName and LastName is required", 404);
            }
            try
            {

                var existingUser = await _userManager.FindByEmailAsync(model.Email.Trim());
                if (existingUser != null)
                {
                    return ApiResponse<object>.ErrorResponse("Email already exists.", null, 409);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email.Trim(),
                    Email = model.Email.Trim(),
                    FirstName = model.FirstName.Trim(),
                    LastName = model.LastName.Trim(),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return ApiResponse<object>.ErrorResponse(string.Join(", ", result.Errors.Select(x => x.Description)), result.Errors, 404);
                }

                return ApiResponse<object>.SuccessResponse(user, "User registered successfully.", 201);
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.ErrorResponse("Internal Server Error.", ex.Message, 500);
            }
        }
    }
}