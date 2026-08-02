using System.Security.Claims;
using Aegis.Model.Auth;
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

    public async Task<IdentityUser?> GetCurrentUserAsync()
    {
      var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
      Console.WriteLine("User Log :",userId , _httpContextAccessor.HttpContext);
      if (string.IsNullOrEmpty(userId))
        return null;

      var user = await _userManager.FindByIdAsync(userId);
      return user;

    }



  }
}