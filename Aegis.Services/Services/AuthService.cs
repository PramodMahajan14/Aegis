
using Aegis.Model.Auth;
using Aegis.Model.DTO.Auth;
using Aegis.Model.Vm.Auth;
using Aegis.Services.Services.Interfaces;
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

        public async Task<RegisterResponse> RegisterAsync(RegisterDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            try
            {


                var existingUser = await _userManager.FindByEmailAsync(model.Email.Trim());

                if (existingUser != null)
                {
                    return new RegisterResponse
                    {
                        Success = false,
                        Errors = true,
                        Message = "Email already exists.",
                        StatusCode = StatusCodes.Status409Conflict
                    };
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
                    return new RegisterResponse
                    {
                        Success = false,
                        Errors = true,
                        Message = string.Join(", ", result.Errors.Select(x => x.Description)),
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                return new RegisterResponse
                {
                    Success = true,
                    Errors = false,
                    Message = "User registered successfully.",
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Errors = true,
                    Message = "An unexpected error occurred.",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}