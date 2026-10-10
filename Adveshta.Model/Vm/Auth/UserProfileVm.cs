using Adveshta.Model.Master;
using Adveshta.Utility.Enum;

namespace Adveshta.Model.Auth
{
    public class UserProfileVm
    {
        public Guid Id {get;set;}

        public string FirstName {get;set;} = string.Empty;

        public string LastName {get;set;} = string.Empty;

        public string Email {get;set;} = string.Empty;

        public Gender Gender {get;set;}
        
        public DateTime JoiningDate { get; set; }

        public bool IsSystem {get;set;} = false;

        public BasicJobRoleVm JobRole {get;set;} = null!;

    }
}