using System.Security.Claims;
using Aegis.Model.Auth;
using Aegis.Model.Employee;
using Aegis.Utility.Common;
using Microsoft.AspNetCore.Identity;
namespace Aegis.Services.Helper
{

  public class UserHelper
  {
    public readonly UserManager<ApplicationUser> _userManager;

    public readonly IHttpContextAccessor _httpContextAccessor;

    public UserHelper(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _userManager = userManager;
    }

    public Guid GetCurrentTenant()
    {
       var tenant = _httpContextAccessor.HttpContext?.User.FindFirst("organization")?.Value;
       return (tenant != null ? GuidUtility.ToGuid(tenant) : Guid.Empty);
    }
    public async Task<IdentityUser?> GetCurrentUserAsync()
    {
      var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

      if (string.IsNullOrEmpty(userId))
        return null;

      var user = await _userManager.FindByIdAsync(userId);
      return user;

    }

    // public async Task<Employee> GetCurrentEmployeeAsync()
    // {
    //   var user = GetCurrentUserAsync();
    //   var tenantId  = GetCurrentTenant();

    //   if(user == null || tenantId ==  Guid.Empty) return new Employee {};



    // }



  }
}