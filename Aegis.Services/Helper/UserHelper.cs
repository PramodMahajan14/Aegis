using System.Security.Claims;
using Aegis.DataAccess.Data;
using Aegis.Model.Auth;
using Aegis.Model.EmployeeModels;
using Aegis.Model.Master;
using Aegis.Utility.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Aegis.Services.Helper
{

  public class UserHelper
  {
    public readonly UserManager<ApplicationUser> _userManager;

    public readonly IHttpContextAccessor _httpContextAccessor;

    public readonly ApplicationDbContext _context;

    public UserHelper(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext conetxt)
    {
      _httpContextAccessor = httpContextAccessor;
      _userManager = userManager;
      _context = conetxt;
    }

    public Guid GetCurrentTenant()
    {
      var tenant = _httpContextAccessor.HttpContext?.User.FindFirst("organization")?.Value;
      return (tenant != null ? GuidUtility.ToGuid(tenant) : Guid.Empty);
    }
    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
      var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

      if (string.IsNullOrEmpty(userId))
        return null;

      var user = await _userManager.FindByIdAsync(userId);
      return user ?? null;

    }

    public async Task<Employee> GetCurrentEmployeeAsync()
    {
      var user = await GetCurrentUserAsync();
      var OrganizationId = GetCurrentTenant();

      if (user == null || OrganizationId == Guid.Empty) return new Employee { };

      var Employee = await _context.Employees.AsNoTracking()
      .Include(a => a.User)
      .Include(a => a.JobRole)
      .FirstOrDefaultAsync(a => a.UserId == user.Id && a.IsActive && a.OrganizationId == OrganizationId);
      return Employee ?? new Employee { };

    }


    public async Task<object?> GetCurrentUserProfile()
    {
      var user = await GetCurrentUserAsync();
      return user == null ? null : new
      {
        user.Id,
        user.UserName,
        user.Email,
        user.PhoneNumber
      };
    }
  }
}