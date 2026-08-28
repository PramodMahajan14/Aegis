
using Aegis.DataAccess.Data;
using Aegis.Model.Auth;
using Aegis.Model.DTO.Auth;
using Aegis.Model.EmployeeModels;
using Aegis.Model.OrganizationModel;
using Aegis.Services.Helper;
using Aegis.Services.Services.Interfaces;
using Aegis.Utility.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aegis.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly RefreshTokenService _refreshTokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EmployeeHelper _employeeHelper;
        private readonly UserHelper _userHelper;
        private readonly ILoggingService _logger;
        private readonly ApplicationDbContext _context;


        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,
        RefreshTokenService refreshTokenService, UserHelper userHelper, EmployeeHelper emphelper, ApplicationDbContext context, ILoggingService logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _refreshTokenService = refreshTokenService;
            _userHelper = userHelper;
            _employeeHelper = emphelper;
            _context = context;
            _logger = logger;
        }


        public async Task<ApiResponse<object>> RegisterAsync(RegisterDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

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


        public async Task<ApiResponse<object>> LoginAsync(LoginDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password))
            {
                return ApiResponse<object>.ErrorResponse(
                    "Invalid credentials",
                    "Email and Password are required.",
                    400);
            }

            try
            {
                // Find user
                var user = await _userManager.FindByEmailAsync(model.Email.Trim());

                if (user == null)
                {
                    return ApiResponse<object>.ErrorResponse(
                        "Invalid credentials",
                        "User not found.",
                        401);
                }

                // Check user status
                if (!user.IsActive)
                {
                    _logger.LogWarning("Login failed: User is blocked: {email} ", model.Email);
                    return ApiResponse<object>.ErrorResponse(
                        "Account disabled",
                        "Your account has been deactivated.",
                        403);
                }

                // Verify password
                var passwordResult = await _signInManager.CheckPasswordSignInAsync(
                    user,
                    model.Password,
                    lockoutOnFailure: false);

                if (!passwordResult.Succeeded)
                {
                    _logger.LogWarning("Login failed: Invalid credentials for UserName or Email: {email}", model.Email);
                    return ApiResponse<object>.ErrorResponse(
                        "Invalid credentials",
                        "Invalid email or password.",
                        401);
                }


                var employee = await _employeeHelper.GetEmployeeByUserId(user.Id);

                if (employee == null)
                {
                    _logger.LogWarning("Login failed: No employee record found for UserId: {UserId}", user.Id);
                    return ApiResponse<object>.ErrorResponse("Employee not found", 404);
                }


                // Generate tokens
                var accessToken = _refreshTokenService.GenerateAccessToken(user);

                var refreshToken = _refreshTokenService.GenerateRefreshToken();

                // Save refresh token
                await _refreshTokenService.SaveRefreshTokenAsync(user.Id, refreshToken);

                // Response
                var response = new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
                _logger.LogInfo("Login Successfully, Employee : {EmployeeId} , UserId :{UserId}", employee.Id, user.Id);
                return ApiResponse<object>.SuccessResponse(
                    response,
                    "Login successful.",
                    200);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Unexpected error during login");
                return ApiResponse<object>.ErrorResponse(
                    "Internal server error.",
                    ex.Message,
                    500);
            }
        }


        public async Task<ApiResponse<object>> GetWorkSpacesAsync(string userId)
        {

            var employee = await _employeeHelper.GetEmployeeByUserId(userId);

            if(employee == null)
            {
                return ApiResponse<object>.ErrorResponse("Employee not found");
            }
            var assignedOrganizationIds =
                await _employeeHelper.GetOrganizationsByEmployeeAsync(employee.Id);

            if (!assignedOrganizationIds.Any())
           
            {
                assignedOrganizationIds.Add(employee.OrganizationId);
            }

            var workspaces = await _context.Organizations
                .Where(o => assignedOrganizationIds.Contains(o.Id))
                .Select(o => new
                {
                    Id = o.Id,
                    Name = o.Name
                })
                .ToListAsync();

            return ApiResponse<object>.SuccessResponse(
                workspaces,
                "Workspaces retrieved successfully.");
        }
        public async Task<ApiResponse<object>> Profile()
        {
            var currentuser = await _userHelper.GetCurrentUserAsync();

            if (currentuser == null)
                return ApiResponse<object>.ErrorResponse("Invalid request", "Please login again - 1", 404);

            var user = await _userManager.FindByIdAsync(currentuser.Id);

            if (user == null)
            {
                return ApiResponse<object>.ErrorResponse("User not found", "Please login again - 2", 404);
            }

            var response = new UserProfileVm()
            {
                Id = GuidUtility.ToGuid(user.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
            };

            return ApiResponse<object>.SuccessResponse(response, "user Fetch successfully", 200);
        }


    }
}