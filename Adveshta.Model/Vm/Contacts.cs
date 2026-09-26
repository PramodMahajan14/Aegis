using Adveshta.Model.Master;
using Adveshta.Model.OrganizationModel;
using Adveshta.Model.Vm.Employee;
using Adveshta.Model.Vm.Prospect;

namespace Adveshta.Model.Vm.Contacts
{
    public class ContactListVm : OrganizationRelation
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        // Person's actual designation
        public string Designation { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string Notes { get; set; } = string.Empty;

        public bool IsPrimary { get; set; } = false;


        public BasicProspectVm Prospect { get; set; } = null!;


        // Role/importance for our project
        public BasicJobRoleVm JobRole { get; set; } = null!;

        public BasicEmployeeVm CreatedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; }


    }
}