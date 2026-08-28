using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Aegis.Model.Auth;
using Aegis.Utility.Enum;
using Aegis.Model.OrganizationModel;
using Aegis.Model.Master;

namespace Aegis.Model.EmployeeModels
{
    public class Employee : BaseCreateUpdate
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = String.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; } = Gender.Male;

        public DateTime JoiningDate { get; set; }

        public bool IsActive { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;

        public string ContactNumber {get;set;} =  string.Empty;

        public bool IsSystem {get;set;} = false;

        public Guid JobRoleId {get;set;}
        [ForeignKey(nameof(JobRoleId))]
        public JobRole JobRole {get;set;} = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public Guid OrganizationId { get; set; }

        [ValidateNever]
        [ForeignKey("OrganizationId")]
        public Organization? Organization { get; set; } = null!;


    }
}