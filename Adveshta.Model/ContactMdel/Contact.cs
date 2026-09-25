using System.ComponentModel.DataAnnotations.Schema;
using Adveshta.Model.EmployeeModels;
using Adveshta.Model.Master;
using Adveshta.Model.OrganizationModel;
using Adveshta.Model.ProspectModel;

namespace Adveshta.Model.ContactModel
{
    public class Contact : OrganizationRelation
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

        public Guid ProspectId { get; set; }

        [ForeignKey(nameof(ProspectId))]
        public Prospect Prospect { get; set; } = null!;


        // Role/importance for our project
        public Guid JobRoleId { get; set; }
        [ForeignKey(nameof(JobRoleId))]
        public JobRole ProjectContactRole { get; set; } = null!;
        

        public Guid CreatedById { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public Employee CreatedBy { get; set; } = null!;



        public Guid? UpdatedById { get; set; }

        [ForeignKey(nameof(UpdatedById))]
        public Employee? UpdatedBy { get; set; }


        public DateTime CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

    }
}