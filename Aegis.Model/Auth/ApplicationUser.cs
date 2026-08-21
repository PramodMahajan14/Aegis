using Microsoft.AspNetCore.Identity;
using Aegis.Model.Employee;
namespace Aegis.Model.Auth;

public class ApplicationUser : IdentityUser
{

    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; }  = String.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Boolean IsRootUser {get;set;}

    public Aegis.Model.Employee.Employee? Employee {get;set;}
}