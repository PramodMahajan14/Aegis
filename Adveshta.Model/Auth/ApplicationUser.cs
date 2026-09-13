using Microsoft.AspNetCore.Identity;
using Adveshta.Model.EmployeeModels;
namespace Adveshta.Model.Auth;

public class ApplicationUser : IdentityUser
{

    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; }  = String.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Boolean IsRootUser {get;set;}

    public Adveshta.Model.EmployeeModels.Employee? Employee {get;set;}
}