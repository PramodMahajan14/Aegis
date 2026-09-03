using System.ComponentModel.DataAnnotations;
using Aegis.Utility.Enum;

namespace Aegis.Model.DTO.Employee
{
    public class EmployeeDto
    {
        public Guid? Id { get; set; }


        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        public string FirstName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Joining date is required.")]
        public DateTime JoiningDate { get; set; }


        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [StringLength(15, ErrorMessage = "Contact number cannot exceed 20 characters.")]
        public string ContactNumber { get; set; } = string.Empty;


        [Required(ErrorMessage = "Gender is required.")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Invalid gender.")]
        public Gender Gender { get; set; }


        [Required(ErrorMessage = "Job role is required.")]
        public Guid JobRoleId { get; set; }
    }
}