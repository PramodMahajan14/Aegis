using Microsoft.AspNetCore.Identity;

namespace Aegis.Model.Auth;
public class ApplicationUser: IdentityUser
{
    public string FirstName {get; set;}

    public string LastName {get; set;}

    public bool IsActive {get; set;}

    public DateTime CreatedAt {get; set;}

    public DateTime? UpdatedAt {get; set;}
}