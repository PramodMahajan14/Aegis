using Adveshta.Model.Master;
using Adveshta.Utility.Enum;

namespace Adveshta.Model.Vm.Employee
{
    public class EmployeeVm
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime JoiningDate { get; set; }

        public string ContactNumber { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public BasicJobRoleVm JobRole { get; set; } = null!;

        public bool IsActive { get; set; }

        public bool IsRoot {get;set;}
    }

    public class BasicEmployeeVm
    {
        public Guid Id {get;set;}

        public string FirstName {get;set;} = string.Empty;

        public string LastName {get;set;} = string.Empty;
    }
}