using Aegis.Utility.Enum;

namespace Aegis.Model.DTO.Employee
{
    public class EmployeeDto
    {
    public Guid? Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string  Email{ get; set; } = string.Empty;

    public DateTime JoiningDate {get;set;}

    public DateTime DateOfBirth {get;set;}

    public Gender Gender {get;set;}
    }
}